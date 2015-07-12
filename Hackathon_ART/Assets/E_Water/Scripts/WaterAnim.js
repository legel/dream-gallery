#pragma strict
var Textures:Texture2D[];
var NormalTextures:Texture2D[];

var NormalMapOn:boolean;

var fps:int;
private var counter:int = 0;

function Start () {

var timer:float = 1.0/fps;

InvokeRepeating("Increment", 0,timer );
}

function Increment () {

counter +=1;

if(counter==Textures.Length){
counter=0;
}

}

function Update () {


gameObject.GetComponent.<Renderer>().material.mainTexture = Textures[counter];

if(NormalMapOn!=true)return;


GetComponent.<Renderer>().material.SetTexture("_BumpMap", NormalTextures[counter]);

}