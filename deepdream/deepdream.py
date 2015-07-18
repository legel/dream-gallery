from cStringIO import StringIO
import numpy as np
import scipy.ndimage as nd
import PIL.Image
from IPython.display import clear_output, Image, display
from google.protobuf import text_format
import caffe


# list of image names to merge, with first name the base, and second name the target
painting_list = [('leonardo_davinci_1', 'van_gogh'), 
                ('van_gogh','salvador_dali' ), 
                ('salvador_dali', 'edward_munch'), 
                ('edward_munch', 'doze_green'), 
                ('doze_green', 'sandro_botticelli'), 
                ('sandro_botticelli', 'picasso'), 
                ('picasso', 'jackson_pollock'), 
                ('jackson_pollock', 'leonardo_davinci_2'), 
                ('leonardo_davinci_2', 'sexer'), 
                ('sexer', 'jennifer_bartlett'), 
                ('jennifer_bartlett', 'georges_seurat'), 
                ('georges_seurat', 'leonardo_davinci_1') ]


model_path = '/caffe-master/models/bvlc_googlenet/' # substitute your path here
net_fn   = model_path + 'deploy.prototxt'
param_fn = model_path + 'bvlc_googlenet.caffemodel'

# see DeepDream documentation for discussion of different layers that can be used...
# here we use layer 3 in a 5 layer network, which captures patterns on the level of "shapes"...
# as opposed to mostly contrasts (1st layer), lines and curves (2nd layer), cooccuring shapes (4th layer)
# and complete objects (5th layer)...
end = 'inception_3b/output'  

for (base_img_name, guide_name) in painting_list:

    # base images dir
    base_img_dir = '/src/original/' + base_img_name + '.jpg'

    # guide images dir
    guide_dir = '/src/original/' + guide_name + '.jpg'

    base_img = np.float32(PIL.Image.open(base_img_dir))
    guide = np.float32(PIL.Image.open(guide_dir))

    def showarray(a, i):
        a = np.uint8(np.clip(a, 0, 255))
        f = StringIO()
        transformed = PIL.Image.fromarray(a)

        # dir to save merged images
        transformed.save("/src/another/" + str(base_img_name) + "-" + str(guide_name) + "_" + str(i) +'.jpg')

    model = caffe.io.caffe_pb2.NetParameter()
    text_format.Merge(open(net_fn).read(), model)
    model.force_backward = True
    open('tmp.prototxt', 'w').write(str(model))

    net = caffe.Classifier('tmp.prototxt', param_fn,
                           mean = np.float32([104.0, 116.0, 122.0]), # ImageNet mean, training set dependent
                           channel_swap = (2,1,0)) # the reference model has channels in BGR order instead of RGB

    # a couple of utility functions for converting to and from Caffe's input image layout
    def preprocess(net, img):
        return np.float32(np.rollaxis(img, 2)[::-1]) - net.transformer.mean['data']

    def deprocess(net, img):
        return np.dstack((img + net.transformer.mean['data'])[::-1])

    def objective_L2(dst):
        dst.diff[:] = dst.data 

    def make_step(net, step_size=5, end=end, jitter=50, clip=True, objective=objective_L2):

        '''Basic gradient ascent step.'''

        src = net.blobs['data'] # input image is stored in Net's 'data' blob
        dst = net.blobs[end]

        ox, oy = np.random.randint(-jitter, jitter+1, 2)
        src.data[0] = np.roll(np.roll(src.data[0], ox, -1), oy, -2) # apply jitter shift

        net.forward(end=end)
        objective(dst)  # specify the optimization objective
        net.backward(start=end)
        g = src.diff[0]
        # apply normalized ascent step to the input image
        src.data[:] += step_size/np.abs(g).mean() * g

        src.data[0] = np.roll(np.roll(src.data[0], -ox, -1), -oy, -2) # unshift image

        if clip:
            bias = net.transformer.mean['data']
            src.data[:] = np.clip(src.data, -bias, 255-bias)    

    def deepdream(net, base_img, iter_n=20, octave_n=1, octave_scale=1.0, end=end, clip=True, **step_params):

        octaves = [preprocess(net, base_img)]
        for i in xrange(octave_n-1):
            octaves.append(nd.zoom(octaves[-1], (1, 1.0/octave_scale,1.0/octave_scale), order=1))

        src = net.blobs['data']
        detail = np.zeros_like(octaves[-1]) # allocate image for network-produced details
        for octave, octave_base in enumerate(octaves[::-1]):
            h, w = octave_base.shape[-2:]
            if octave > 0:
                # upscale details from the previous octave
                h1, w1 = detail.shape[-2:]
                detail = nd.zoom(detail, (1, 1.0*h/h1,1.0*w/w1), order=1)

            src.reshape(1,3,h,w) # resize the network's input image size
            src.data[0] = octave_base+detail
            for i in xrange(iter_n):
                make_step(net, end=end, clip=clip, **step_params)

                vis = deprocess(net, src.data[0])
                if not clip: # adjust image contrast if clipping is disabled
                    vis = vis*(255.0/np.percentile(vis, 99.98))
                showarray(vis, i=i)
                print octave, i, end, vis.shape
                clear_output(wait=True)

            # extract details produced on the current octave
            detail = src.data[0]-octave_base
        # returning the resulting image
        return deprocess(net, src.data[0])

    h, w = guide.shape[:2]
    src, dst = net.blobs['data'], net.blobs[end]
    src.reshape(1,3,h,w)
    src.data[0] = preprocess(net, guide)
    net.forward(end=end)
    guide_features = dst.data[0].copy()

    def objective_guide(dst):
        x = dst.data[0].copy()
        y = guide_features
        ch = x.shape[0]
        x = x.reshape(ch,-1)
        y = y.reshape(ch,-1)
        A = x.T.dot(y) # compute the matrix of dot-products with guide features
        dst.diff[0].reshape(ch,-1)[:] = y[:,A.argmax(1)] # select ones that match best


    _=deepdream(net, base_img, end=end, objective=objective_guide)


# END
