<template lang="pug">
  el-container.index
    el-header.header(height="50px")
      h2.display-inline-block RemarkTool
      .userInfo.display-inline-block
        span.username
          i.el-icon-Mark-user
          span {{user.name}} 
        span.saperate |
        .taskInfo.display-inline-block
          span {{currentTask.name}}
          span.text-success {{currentTask.annotatedfilenum}} 
          span / {{currentTask.totalfilenum}}
    el-container.container.fill-height
      el-container.workspace.fill-height
        el-header.toolbar(height="36px")
          el-button(size="mini") Get Bucket
          .timer.display-inline-block 
            span.display-inline-block  time
            .display-inline-block 
              el-input(placeholder="请输入时间" v-model="time" size="mini")
                template(slot="append") S
          .timeLeft.display-inline-block
            i.el-icon-Mark-Time
            span {{timeLeft}}
          .bucketList(:class="{'opened': bucketListIsOpen}")
            .openList.pointer(@click="openBucketList" )
              .sanjiaoxing
            .bucket.pointer(v-for="bucket in bucketList" :class="{'selected': bucket.selected}" @click="toggleBucket(bucket)")
              span.bucketName {{bucket.name}}
              span.pull-right {{bucket.totalfilenum}}
        el-container.fill-height
          el-container.fill-height.workArea(style="overflow:hidden")
            el-container 
              markCanvas             
            el-footer.imagelocationInfo(:class="{'show':imageInfoIsShow}")
              .title  MetaData
                i.el-icon-back.showImageInfo.pointer.pull-right(@click="showImageInfo()")  
              el-row.MetaData(:gutter="20") 
                el-col.metaDetial(:span="8" v-for="(meta,key) in metaData" key="meta")
                  el-row
                    el-col(:span="8") {{key}}:  
                    el-col(:span="16")
                      el-input(value="meta" size="mini")
          el-aside.tools(width="36px")          
            .toolBtn.pointer(v-for="tool in toolList" @click="toolsClick(tool)" :class="{selected:tool.selected}" )
              i(:class="tool.icon")
      el-aside.sidebar
        .setInfo.pointer
          i.el-icon-Mark-Set
        el-tabs.fill-height(v-model="selectedTab" @tab-click="toggleTabs")
          el-tab-pane(label="分类" name="class") 
            ul.typeList
              li.typeItem.pointer(v-for="(type, index) in typeList" @click="selectedType(type)" :class="{'selected':type.selected}") {{type.name}}
                span.keymap.pull-right {{index+1}}
              li.typeItem.addType.pointer(@click="addType(type)")  + 新增分类
          el-tab-pane.mark(label="标注" name="mark") 
            //- .markType
              //- span.markTypeItem.display-inline-block(v-for="type in markType" :class="{selected:type.selected}") {{type.name}}
            el-radio-group.markType(v-model="selectedMarkType")
              el-radio-button.markTypeItem(v-for="type in markType" key="type.name" :label="type.name")
            .ImageInfo    
              .infoAttr(v-for="info in imageInfo")
                p {{info.attribute}} 
                el-radio-group(v-model="info.value")
                  el-radio-button.markTypeItem(v-for="v in info.preValue" key="v.value" :label="v.value")
</template>

<script>
import * as d3 from "d3";
import markCanvas from "./mark-canvas";
  export default {
    name: 'index',
    components: {
      markCanvas:markCanvas
    },
    data (){
      return {
        user:{
          name:'王小米'          
        }        
        ,currentTask :{
          name:'car',
          totalfilenum:320,
          annotatedfilenum:202
        }
        , time:''
        , timeLeft:10
        , selectedTab:'mark'
        , toolList:[
          {
            id:0
            , name:'save'
            , toolTip:'save'
            , icon:'el-icon-Mark-Set'     
            , mothod:'save'       
            , selected:true
          },
          {
            id:1
            , name:'enlarge'
            , toolTip:'enlarge'
            , icon:'el-icon-Mark-Amplification'
            , mothod:'enlarge' 
            , selected:false
          },
          {
            id:2
            , name:'narrow'
            , toolTip:'narrow'
            , icon:'el-icon-Mark-Narrow'
            , mothod:'narrow' 
            , selected:false
          },
          {
            id:3
            , name:'AdaptiveHeight'
            , toolTip:'AdaptiveHeight'
            , icon:'el-icon-Mark-AdaptiveHeight'
            , mothod:'AdaptiveHeight' 
            , selected:false
          },
          {
            id:4
            , name:'AdaptiveWidth'
            , toolTip:'AdaptiveWidth'
            , icon:'el-icon-Mark-AdaptiveWidth'
            , mothod:'AdaptiveWidth' 
            , selected:false
          },
          {
            id:5
            , name:'OriginalSize'
            , toolTip:'OriginalSize'
            , icon:'el-icon-Mark-OriginalSize'
            , mothod:'OriginalSize' 
            , selected:false
          },
          {
            id:6
            , name:'move'
            , toolTip:'move'
            , icon:'el-icon-Mark-Mobile'
            , mothod:'move' 
            , selected:false
          },
          {
            id:7
            , name:'brush'
            , toolTip:'Brush'
            , icon:'el-icon-Mark-Brush'
            , mothod:'brush' 
            , selected:false
          },
          {
            id:8
            , name:'next'
            , toolTip:'next'
            , icon:'el-icon-Mark-Right'
            , mothod:'previous' 
            , selected:false
          },
          {
            id:9
            , name:'previous'
            , toolTip:'previous'
            , icon:'el-icon-Mark-Left'
            , mothod:'previous' 
            , selected:false
          }
        ]
        , typeList:[
          {
            id:'0'
            , name:'type1'
            , selected: true
          }
          ,{
            id:'0'
            , name:'type2'
            , selected: false
          },
          {
            id:'0'
            , name:'type3'
            , selected: false
          },{
            id:'0'
            , name:'type4'
            , selected: false
          }
        ]
        , newType:{
          id:''
          , name :''
          , selected:false
        }
        , markType:[
          {
            name:'Car'
            , id:'0'
            , selected: false
          },
          {
            name:'Bus'
            , id:'0'
            , selected: false
          },
          {
            name:'Truck'
            , id:'0'
          },
          {
            name:'Truck Trailer'
            , id:'0'
            , selected: false
          },
          {
            name:'Bicycle'
            , id:'0'
            , selected: false
          },
          {
            name:'Motorcycle'
            , id:'0'
            , selected: false
          },
          {
            name:'Cyclist'
            , id:'0'
            , selected: false
          },
          {
            name:'Pedestrian'
            , id:'0'
            , selected: false
          },
          {
            name:'Traffic Signs'
            , id:'0'
            , selected: false
          },
          {
            name:'Info Signs'
            , id:'0'
            , selected: false
          },
          {
            name:'Special'
            , id:'0'
            , selected: false
          },
          {
            name:"Don't care"
            , id:'0'
            , selected: false
          }
        ]
        ,selectedMarkType:''
        ,imageInfo:[
          {
            attribute:'CVIP',
            value:"",
            preValue: [
              {
                selected:false,
                value:0
              }
              ,{
                selected:false,
                value:1
              }
            ]
          }
          ,{
            attribute:'Ignore_anglepoint',
            value:"",
            preValue:  [
              {
                selected:false,
                value:0
              }
              ,{
                selected:false,
                value:1
              }
            ]
          }
          ,{
            attribute:'Occluded',
            value:"",
            preValue: [
              {
                selected:false,
                value:0
              }
              ,{
                selected:false,
                value:0.2
              }
              ,{
                selected:false,
                value:0.4
              }
              ,{
                selected:false,
                value:0.6
              }
              ,{
                selected:false,
                value:0.8
              }
              ,{
                selected:false,
                value:1
              }
            ]
          } 
          ,{
            attribute:'Orientation',
            value:"",
            preValue: [
              {
                selected:false,
                value:'front'
              }
              ,{
                selected:false,
                value:'rear'
              }
              ,{
                selected:false,
                value:'left'
              }
              ,{
                selected:false,
                value:'right'
              }
              ,{
                selected:false,
                value:'left-front'
              }
              ,{
                selected:false,
                value:'right-font'
              }
              ,{
                selected:false,
                value:'left-rear'
              }
              ,{
                selected:false,
                value:'right-rear'
              }
            ]
          }
        ]
        ,imageInfoIsShow:false
        , metaData: {
          Date:'7/27/2017'
          , Country: 'China'
          , Weather: 'Sunnuy'
          , Time :'12:10:06 AM'
          , 'Provience/State': 'Hubei'
          , 'Highway/Local': 'Local'
          , Gps_Latitude: '114.429793'
          , City: '30.429793'
          , Road_Name: 'Guanggu Sanlu'
        }
        , bucketList:[
          {
            uuid:'02215544'
            , name: 'Car'
            , annotatedfilenum: 200
            , totalfilenum: 320
            , selected: true
          }
          ,{
            uuid:'0221511544'
            , name: 'eyesoct'
            , annotatedfilenum: 2000
            , totalfilenum: 3100
            , selected : false
          }
          ,{
            uuid:'02215114'
            , name: 'bucket'
            , annotatedfilenum: 2000
            , totalfilenum: 3100
            , selected : false
          }
        ]
        , bucketListIsOpen: false
      }
    },
    methods: {      
      changeSize (type){
        console.log('changeSize:',type)
      },
      save (){
        console.log('save')
      },
      enlarge (){
        console.log('enlarge:',)
        // this.changeSize()
      },
      narrow (){
        console.log('narrow:',)
      },
      AdaptiveHeight (){
        console.log('AdaptiveHeight:',)
      },
      AdaptiveWidth (){
        console.log('AdaptiveWidth:',)
      },
      OriginalSize (){
        console.log('OriginalSize:',)
      },
      move(){
        console.log('move')
      },
      brush(){
        console.log('brush')
      },
      next(){
        console.log('next')
      },
      previous(){
        console.log('previous')
      },      
      toolsClick(tool){
        console.log(tool)
        this.toolList.forEach(function(t,i){
          if(t.name != tool.name){
            t.selected = false
          }else{
            t.selected = true
          }
        })
        this[tool.mothod]()
      },
      selectedType (type){
        this.typeList.forEach(function(t){
          if(t.name != type.name){
            t.selected = false
          }else{
            t.selected = true
          }
        })
        type.selected  = true
      },
      toggleTabs (tab,event){
        this.selectedTab = tab.name
        // console.log(tab)
      },
      addType (){

      },
      showImageInfo(){
        console.log(this.imageInfoIsShow)
        this.imageInfoIsShow = !this.imageInfoIsShow
      }
      ,openBucketList(){
        this.bucketListIsOpen = !this.bucketListIsOpen
      },
      toggleBucket(bucket){
        this.bucketList.forEach(function(b){
          b.selected = b.name == bucket.name
        })
        this.currentTask = bucket
        this.openBucketList()
      }
    }
    ,mounted (){
      console.log('mounted:',this.$electron)      
    }
  }
</script>

<style lang="scss">
  .index{
    .header{
      border-bottom: 2px solid #2a2a2a;
      line-height: 50px;
      color: #fff;
      h2{
        width:120px;
      }
      .saperate{
        padding: 0px 10px;
      }
      .taskInfo{
        span:first-of-type{
          padding-right:5px;
        }
      }
    }
    >.container{
      width: 100%;
      position: absolute;
      left: 0;
      top:-0;
      padding-top:50px;
      .workspace{
        border-right: 1px solid #2a2a2a;
        background-color: #383838;
        .toolbar{
          line-height: 36px;
          background-color: #424242;
          border-bottom:1px solid #2a2a2a; 
          color: #fff;
          position: relative;
          .timer{
            padding:0 10px;
            >span{
              padding-right:10px;
            }
          }
          .bucketList{
            position: absolute;
            background-color: #424242;
            left: -120px;
            width: 120px;            
            z-index: 1;
            border:1px solid #2a2a2a;
            transition: all 0.5s; 
            .openList{
              background-color: #424242;
              position: absolute;
              left: 118px;
              width: 20px;
              height: 30px;
              top: -1px;
              border:1px solid #2a2a2a;
              .sanjiaoxing{
                width: 0px;
                height: 0px;
                border: 10px solid transparent;
                border-bottom-color: #fff;
                transform: rotate(90deg);
                position: absolute;
                left: 0px;
                top:0px;
                transition: all 0.5s;   
                transform-origin:50% 75%;             
              }                          
            }
            .bucket{
              padding: 0px 13px;
              &:hover,&.selected{
                background-color: #383838;
                color: #409eff
                // .bucketName{
                //   colo
                // }
              }
            }  
            &.opened{
              left: 0 ;
              .sanjiaoxing{
                transform: rotate(270deg)
              }                  
            }
          }
        }
        .tools{
          border-left: 1px solid #2a2a2a;
          color: #fff;
          background-color: #424242;
          .toolBtn{
            height: 33px;
            text-align: center;   
            margin:2px;
            border:1px solid transparent;
            i{
              font-size: 28px;
              line-height: 31px;
            }
            &.selected{
              border:1px solid #fff;
              border-radius: 5px;
            }
          }
        }
        .workArea{
          overflow: hidden;
          position: relative;
          .imagelocationInfo{
            border-top:1px solid #2a2a2a;
            background-color: #4d4d4d;
            padding-right: 2px;
            position: absolute;
            width: 100%;
            bottom:0;
            height: 24px !important;
            transition: all 0.5s;
            .title{
              color: #fff;
              font-size: 16px;
              height: 24px;
              line-height: 24px;
            }
            .showImageInfo{
              color: #fff;
              transform: rotate(135deg);
              font-size:24px;
              transition: all 0.5s;
            }
            .MetaData{
              padding-right: 20px;
            }
            .metaDetial{
              padding:10px 0;
              color:#fff;
              height: 40px;
              line-height: 40px;
            }
            &.show{
              height: 170px !important;
              .showImageInfo{
                transform: rotate(315deg);
              }
            }
          }
        }        
      }
      .sidebar{
        background-color: #4d4d4d;
        position: relative;
        .el-tabs__item{
          width: 120px;
          text-align: center;
          padding:0;
        }
        .el-tabs__active-bar{
          width: 100px !important;
        }
        .setInfo{
          position:absolute;
          right: 15px;
          top: 0;
          z-index: 999;
          color:#fff;
          width: 36px;
          height: 36px;
          text-align: center;

          i{
            font-size: 28px;
            line-height: 36px;
          }
         
        }
        .typeList{
          list-style: none;
          width: 100%;
          padding:0 20px;
          .typeItem{
            border: 1px solid #2a2a2a;
            background-color: #383838;
            height: 40px;
            line-height: 40px;
            margin-bottom:10px; 
            padding:0 10px;
            color: #fff;
            &.selected{
              background-color: #52bacf
            }
            &.addType{
              border-style: dashed;
              text-align: center;
              background-color: #4d4d4d
            }
          }
        }
        .mark{
          .markType{
            border-bottom: 1px solid #2a2a2a;          
            
          }
          .markTypeItem{
            background-color: #424242;
            border:1px solid #2a2a2a;
            height: 36px;
            width:130px;
            margin:2px 10px;
            text-align: center;
            line-height:38px;
            color: #fff;
            &.selected{
              background-color: #409EFF;
            }
            &.is-active{
              background-color: #409EFF;
            }
          }
          .el-radio-button__inner{
            background-color: transparent;
            border: none;
            color: #fff;
            -webkit-box-shadow:none;
            box-shadow: none;
          }
        }
        
      }      
    }
    
  }
</style>