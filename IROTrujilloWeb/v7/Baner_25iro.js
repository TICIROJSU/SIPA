(function (lib, img, cjs, ss, an) {

var p; // shortcut to reference prototypes
lib.webFontTxtInst = {}; 
var loadedTypekitCount = 0;
var loadedGoogleCount = 0;
var gFontsUpdateCacheList = [];
var tFontsUpdateCacheList = [];
lib.ssMetadata = [
		{name:"Baner_25iro_atlas_", frames: [[0,0,1024,768],[1026,537,642,309],[0,770,1024,690],[1026,848,520,380],[1574,0,285,303],[1026,0,546,535]]}
];



lib.updateListCache = function (cacheList) {		
	for(var i = 0; i < cacheList.length; i++) {		
		if(cacheList[i].cacheCanvas)		
			cacheList[i].updateCache();		
	}		
};		

lib.addElementsToCache = function (textInst, cacheList) {		
	var cur = textInst;		
	while(cur != exportRoot) {		
		if(cacheList.indexOf(cur) != -1)		
			break;		
		cur = cur.parent;		
	}		
	if(cur != exportRoot) {		
		var cur2 = textInst;		
		var index = cacheList.indexOf(cur);		
		while(cur2 != cur) {		
			cacheList.splice(index, 0, cur2);		
			cur2 = cur2.parent;		
			index++;		
		}		
	}		
	else {		
		cur = textInst;		
		while(cur != exportRoot) {		
			cacheList.push(cur);		
			cur = cur.parent;		
		}		
	}		
};		

lib.gfontAvailable = function(family, totalGoogleCount) {		
	lib.properties.webfonts[family] = true;		
	var txtInst = lib.webFontTxtInst && lib.webFontTxtInst[family] || [];		
	for(var f = 0; f < txtInst.length; ++f)		
		lib.addElementsToCache(txtInst[f], gFontsUpdateCacheList);		

	loadedGoogleCount++;		
	if(loadedGoogleCount == totalGoogleCount) {		
		lib.updateListCache(gFontsUpdateCacheList);		
	}		
};		

lib.tfontAvailable = function(family, totalTypekitCount) {		
	lib.properties.webfonts[family] = true;		
	var txtInst = lib.webFontTxtInst && lib.webFontTxtInst[family] || [];		
	for(var f = 0; f < txtInst.length; ++f)		
		lib.addElementsToCache(txtInst[f], tFontsUpdateCacheList);		

	loadedTypekitCount++;		
	if(loadedTypekitCount == totalTypekitCount) {		
		lib.updateListCache(tFontsUpdateCacheList);		
	}		
};
// symbols:



(lib.biometriaoculardiagnosticoprevioacirugiadecataratas = function() {
	this.spriteSheet = ss["Baner_25iro_atlas_"];
	this.gotoAndStop(0);
}).prototype = p = new cjs.Sprite();



(lib.campaña_iro = function() {
	this.spriteSheet = ss["Baner_25iro_atlas_"];
	this.gotoAndStop(1);
}).prototype = p = new cjs.Sprite();



(lib.IMG20190708WA0001 = function() {
	this.spriteSheet = ss["Baner_25iro_atlas_"];
	this.gotoAndStop(2);
}).prototype = p = new cjs.Sprite();



(lib.iniciooftalmologia = function() {
	this.spriteSheet = ss["Baner_25iro_atlas_"];
	this.gotoAndStop(3);
}).prototype = p = new cjs.Sprite();



(lib.Logo_25_años_iro_ = function() {
	this.spriteSheet = ss["Baner_25iro_atlas_"];
	this.gotoAndStop(4);
}).prototype = p = new cjs.Sprite();



(lib.surgery3 = function() {
	this.spriteSheet = ss["Baner_25iro_atlas_"];
	this.gotoAndStop(5);
}).prototype = p = new cjs.Sprite();
// helper functions:

function mc_symbol_clone() {
	var clone = this._cloneProps(new this.constructor(this.mode, this.startPosition, this.loop));
	clone.gotoAndStop(this.currentFrame);
	clone.paused = this.paused;
	clone.framerate = this.framerate;
	return clone;
}

function getMCSymbolPrototype(symbol, nominalBounds, frameBounds) {
	var prototype = cjs.extend(symbol, cjs.MovieClip);
	prototype.clone = mc_symbol_clone;
	prototype.nominalBounds = nominalBounds;
	prototype.frameBounds = frameBounds;
	return prototype;
	}


(lib.personalIRO = function(mode,startPosition,loop) {
	this.initialize(mode,startPosition,loop,{});

	// Layer 1
	this.instance = new lib.IMG20190708WA0001();
	this.instance.parent = this;
	this.instance.setTransform(0,0,1.358,1.358);

	this.timeline.addTween(cjs.Tween.get(this.instance).wait(1));

}).prototype = getMCSymbolPrototype(lib.personalIRO, new cjs.Rectangle(0,0,1390.6,937), null);


(lib._25aa = function(mode,startPosition,loop) {
	this.initialize(mode,startPosition,loop,{});

	// Layer 1
	this.instance = new lib.Logo_25_años_iro_();
	this.instance.parent = this;

	this.timeline.addTween(cjs.Tween.get(this.instance).wait(1));

}).prototype = getMCSymbolPrototype(lib._25aa, new cjs.Rectangle(0,0,285,303), null);


// stage content:
(lib.Baner_25iro = function(mode,startPosition,loop) {
	this.initialize(mode,startPosition,loop,{});

	// timeline functions:
	this.frame_230 = function() {
		this.stop();
	}

	// actions tween:
	this.timeline.addTween(cjs.Tween.get(this).wait(230).call(this.frame_230).wait(1));

	// Layer 3
	this.instance = new lib._25aa();
	this.instance.parent = this;
	this.instance.setTransform(1280,468.8,2.666,2.666,0,0,0,142.5,151.6);
	this.instance.alpha = 0;

	this.timeline.addTween(cjs.Tween.get(this.instance).to({regX:142.6,regY:151.5,scaleX:1.6,scaleY:1.6,x:1280.3,y:468.6,alpha:1},46).to({_off:true},1).wait(99).to({_off:false,regY:151.6,y:468.7},0).to({regX:142.5,x:1280.1},6).to({regY:151.5,scaleX:1,scaleY:1,x:747.6,y:166.1},24).wait(55));

	// Text1
	this.text = new cjs.Text("La mejor Tecnología...", "italic bold 90px 'Times New Roman'", "#003366");
	this.text.lineHeight = 102;
	this.text.lineWidth = 863;
	this.text.alpha = 0.72549020;
	this.text.parent = this;
	this.text.setTransform(32.5,39.9);

	this.text_1 = new cjs.Text("Al cuidado de tu Salud Ocular..!", "bold 90px 'Times New Roman'", "#003366");
	this.text_1.lineHeight = 102;
	this.text_1.lineWidth = 653;
	this.text_1.alpha = 0.72549020;
	this.text_1.parent = this;
	this.text_1.setTransform(1862.7,713.9);

	this.timeline.addTween(cjs.Tween.get({}).to({state:[]}).to({state:[{t:this.text_1},{t:this.text}]},48).to({state:[]},48).wait(135));

	// Layer 1
	this.instance_1 = new lib.surgery3();
	this.instance_1.parent = this;
	this.instance_1.setTransform(757,-44,1.916,1.916);
	this.instance_1._off = true;

	this.timeline.addTween(cjs.Tween.get(this.instance_1).wait(47).to({_off:false},0).to({_off:true},24).wait(160));

	// Layer 6
	this.instance_2 = new lib.biometriaoculardiagnosticoprevioacirugiadecataratas();
	this.instance_2.parent = this;
	this.instance_2.setTransform(768,85);
	this.instance_2._off = true;

	this.timeline.addTween(cjs.Tween.get(this.instance_2).wait(71).to({_off:false},0).to({_off:true},25).wait(135));

	// Text2
	this.text_2 = new cjs.Text("Siempre al servicio de la comunidad..", "italic bold 90px 'Times New Roman'", "#003366");
	this.text_2.lineHeight = 102;
	this.text_2.lineWidth = 1435;
	this.text_2.alpha = 0.70980392;
	this.text_2.parent = this;
	this.text_2.setTransform(22,17.2);

	this.text_3 = new cjs.Text("a travez de campañas y operaciones gratuitas..!", "bold 90px 'Times New Roman'", "#003366");
	this.text_3.lineHeight = 102;
	this.text_3.lineWidth = 1860;
	this.text_3.alpha = 0.70980392;
	this.text_3.parent = this;
	this.text_3.setTransform(691.9,817);

	this.timeline.addTween(cjs.Tween.get({}).to({state:[]}).to({state:[{t:this.text_3},{t:this.text_2}]},96).to({state:[]},50).wait(85));

	// Layer 5
	this.instance_3 = new lib.campaña_iro();
	this.instance_3.parent = this;
	this.instance_3.setTransform(580,131,2.183,2.183);
	this.instance_3._off = true;

	this.timeline.addTween(cjs.Tween.get(this.instance_3).wait(96).to({_off:false},0).to({_off:true},25).wait(110));

	// Layer 9
	this.instance_4 = new lib.iniciooftalmologia();
	this.instance_4.parent = this;
	this.instance_4.setTransform(667,159,1.596,1.596);
	this.instance_4._off = true;

	this.timeline.addTween(cjs.Tween.get(this.instance_4).wait(121).to({_off:false},0).to({_off:true},25).wait(85));

	// Layer 2
	this.instance_5 = new lib.personalIRO();
	this.instance_5.parent = this;
	this.instance_5.setTransform(1280.1,468.5,1,1,0,0,0,695.3,468.5);
	this.instance_5.alpha = 0.102;
	this.instance_5._off = true;

	this.timeline.addTween(cjs.Tween.get(this.instance_5).wait(146).to({_off:false},0).to({alpha:1},63).wait(22));

}).prototype = p = new cjs.MovieClip();
p.nominalBounds = new cjs.Rectangle(2180.1,533.1,759.9,807.9);
// library properties:
lib.properties = {
	width: 2560,
	height: 937,
	fps: 24,
	color: "#FFFFFF",
	opacity: 1.00,
	webfonts: {},
	manifest: [
		{src:"images/Baner_25iro_atlas_.png", id:"Baner_25iro_atlas_"}
	],
	preloads: []
};




})(lib = lib||{}, images = images||{}, createjs = createjs||{}, ss = ss||{}, AdobeAn = AdobeAn||{});
var lib, images, createjs, ss, AdobeAn;