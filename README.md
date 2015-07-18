## Dream Gallery

### Inspiration
What happens when Mona Lisa meets a Starry Night?  We used a computer vision library called  [DeepDream](http://googleresearch.blogspot.com/2015/06/inceptionism-going-deeper-into-neural.html) to make wonderful new fusions of art.  To present these *dreams* we developed an interactive and immersive virtual reality art gallery. 

### How it works
<p style="text-align: center;"><span style="font-family:georgia,serif"><img alt="" src="https://raw.githubusercontent.com/legel/dream-gallery/master/dreams/transformed/leonardo_davinci_1-van_gogh_14.jpg" /></span></p>

Our code is based on a deep neural network, which first considers an input "base" image (*e.g.* Mona Lisa), and then a target "guide" image (*e.g.* Starry Night). 
<p style="text-align: center;"><img alt="" src="https://raw.githubusercontent.com/legel/dream-gallery/master/wiki/mona_lisa_starry_night.png" style="height:410px; width:800px" /></p>

Basically, it looks for patterns in the first image that remind it of the second image, and then imagines projections of the second image's patterns onto these first image's patterns.

<p style="text-align: center;"><img alt="" src="https://raw.githubusercontent.com/legel/dream-gallery/master/gallery/renders/dream_gallery_1.png" style="height:366px; width:800px" /></p>

For the actual gallery, we embedded 
[12 works of art](https://github.com/legel/dream-gallery/tree/master/dreams/original) into a Unity3D world.

<p style="text-align: center;"><img alt="" src="https://raw.githubusercontent.com/legel/dream-gallery/master/gallery/renders/dream_gallery_2.png" style="height:366px; width:800px" /></p>
We coded [LeapVR](https://www.leapmotion.com/product/vr) hand interaction, so a user could select 2 pieces of art to fuse by DeepDream in real-time.

### Challenges
We made movies of the art fusions - *e.g.* ["Starry Lisa"](https://youtu.be/ejAgy0uhdHM), ["Disintegration of Screams"](https://youtu.be/eMIvC4DtIMw), ["Surrounded by Artificial Houses"](https://youtu.be/ejAgy0uhdHM) - but didn't have time during the 48 hour [hackathon](http://timewavefestival.com/hackathon-hackerteams.html) to embed them into Unity3D with LeapVR interaction.

### Highlights
<p style="text-align:center"><img alt="" src="https://raw.githubusercontent.com/legel/dream-gallery/master/dreams/transformed/edward_munch-doze_green_19.jpg" style="height:631px; width:500px" /></p>
Projecting modern artist [Doze Green](http://jonathanlevinegallery.com/?method=Exhibit.ExhibitDescriptionPast&ExhibitID=4FCDAB7A-9091-BAA3-8A48DEA92479CE4A) onto The Scream makes it all the more scary.
<p style="text-align:center"><img alt="" src="https://raw.githubusercontent.com/legel/dream-gallery/master/wiki/edwardmunch_dozegreen.png" style="height:521px; width:800px" /></p>

<p style="text-align:center"><img alt="" src="https://raw.githubusercontent.com/legel/dream-gallery/master/dreams/transformed/leonardo_davinci_2-jackson_pollock_19.jpg" style="height:613px; width:500px" /></p>
Leonardo da Vinci's sketches take on new abstractions after being infused with work by Jackson Pollock.
<p style="text-align:center"><img alt="" src="https://raw.githubusercontent.com/legel/dream-gallery/master/wiki/Da%20Vinci%20Pollack.png" style="height:367px; width:800px" /></p>

<p style="text-align:center"><img alt="" src="https://raw.githubusercontent.com/legel/dream-gallery/master/dreams/transformed/sexer-jennifer_bartlett_19.jpg" style="height:418px; width:500px" /></p>

Work by New York City street artist [Sexer](https://instagram.com/sexer1/) is made even more "artificial" with help from MOMA artist [Jennifer Bartlett](http://www.moma.org/collection/artists/357).
<p style="text-align:center"><img alt="" src="https://raw.githubusercontent.com/legel/dream-gallery/master/wiki/Sexer%20Bartlett.png" style="height:363px; width:800px" /></p>

### Conclusion
We used artificial intelligence to create art for a virtual gallery.  [Sweet dreams](https://github.com/legel/dream-gallery/tree/master/dreams/transformed).
