<template lang="pug">
  div#markScreen(ref="canvasScreen")
    div#markBox(ref='markBox' :style="{left:path.startX + 'px',top:path.startY + 'px', width:path.endX + 'px',height:path.endY + 'px'}" )
    canvas#markCanvasBak(ref="canvasBak" width="750" height="500" @mousedown="canvasMousedown" @mouseup="canvasMouseup" @mousemove="canvasMousemove" :class="{'crosshair': isMarking}")
</template>

<script>
import * as d3 from "d3";
export default {
  name: 'index',
  data (){
    return {
      canvas:{}
      ,canvasBak:{}
      ,context:{}
      ,contextBak:{}
      ,overMark:null
      ,stage_info:{}
      ,isMarking:false
      ,canvasStart:{}
      ,currentWorkImage :{}
      ,canvasScreen:{}
      ,path:{
        startX:0
        ,startY:0
        , endX:0
        , endY:0
      }
      ,markList:[
        {
          startX:0
          ,startY:0
          , endX:10
          , endY:10
        }
      ]
      ,currentWork:'http://localhost:9080/static/car.jpg'
    }
  },
  methods: { 
    canvasMousedown(e){     
      this.stage_info = this.canvas.getBoundingClientRect()
      this.isMarking = true
      this.canvasStart.x = e.pageX
      this.canvasStart.y = e.pageY
      this.path.startX = e.pageX-this.stage_info.left
      this.path.startY = e.pageY-this.stage_info.top
    },
    canvasMouseup (e){      
      this.isMarking = false
      this.canvasStart ={}
      this.contextBak.clearRect(0,0,750,500)
      this.context.rect(this.path.startX,this.path.startY,this.path.endX ,this.path.endY)
      this.context.stroke()
    },
    canvasMousemove(e){        
      if(this.isMarking ){
        this.path.endX = e.pageX-this.canvasStart.x
        this.path.endY = e.pageY-this.canvasStart.y
            // 
        // console.log(this.canvasStart.x-this.stage_info.left,this.canvasStart.y-this.stage_info.top,e.pageX-this.canvasStart.x,e.pageY-this.canvasStart.y)
        // console.log(this.path.startX,this.path.startY,this.path.endX ,this.path.endY)
        // // this.context.clearRect(this.canvasStart.x-this.stage_info.left,this.canvasStart.y-this.stage_info.top,e.pageX-this.canvasStart.x,e.pageY-this.canvasStart.y)
        // // this.context.clearRect(this.path.startX+1,this.path.startY+1,this.path.endX-2 ,this.path.endY-2 )
        // // this.context.stroke()
        // // this.contextBak.clearRect(this.path.startX,this.path.startY,this.path.endX ,this.path.endY)
        // // this.contextBak.fillRect(this.path.startX,this.path.startY,this.path.endX ,this.path.endY )
        // var box = this.$refs.markBox
        // console.log('box:',[box])
        // box.style.top = this.path.startY +'px'
        // box.style.left = this.path.startX+'px'
        // box.style.width = this.path.endX+'px'
        // box.style.height = this.path.endY+'px'
        // debugger
        // console.log('box:',[box])
      }        
    }
  }
  ,mounted (){
    console.log('mounted:',this.$electron)
    // console.log('mounted this:',this)
    this.canvasScreen = this.$refs.canvasScreen
    this.canvas = this.$refs.canvas
    this.canvasBak = this.$refs.canvasBak
    this.context = this.canvas.getContext('2d')
    this.contextBak = this.canvasBak.getContext('2d')
    
    // console.log('canvas:', [this.canvas])
    this.context.strokeStyle="blue";
    this.context.lineWidth="1";
  }
}
</script>

<style lang="scss">
  #markScreen{
    position: relative;
    width: 752px;
    height: 502px;
    overflow: hidden;
    top:50%;
    margin-top:-250px;
    left: 50%;
    margin-left: -375px;
    .currentWorkImg{
      width: 100%;
      height: 100%;
      // float: left;
    }
    #markCanvas{
      // height: 500px;
      // width: 750px;
      position: absolute;
      top: 0;
      left: 0;
      border:1px solid red;
    }
    #markBox{
      position: absolute;
      top: 0;
      left: 0;
      border:1px solid red;
    }
    #markCanvasBak{
      // height: 500px;
      // width: 750px;
      position: absolute;
      top: 0;
      left: 0;
      border:1px solid red;
    }
    .crosshair{
      cursor: crosshair;
    }
  }
</style>