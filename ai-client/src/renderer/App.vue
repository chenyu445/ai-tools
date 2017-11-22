<template lang="pug">
  #app
    router-view
</template>

<script>

export default {
  name: 'app'
}
</script>
<style rel="stylesheet/scss" lang="scss">
@import '~normalize.css/normalize.css';// normalize.css 样式格式化
@import url("./assets/iconfont/iconfont.css"); //图标字体

  $light-blue: #58B7FF;
  $blue: #20a0ff;
  $dark-blue:#1D8CE0;

  $sucess: #13ce66;
  $warning:#f7ba2a;
  $danger:#ff4949;

  $black:#1f2d3d;
  $light-dark:#324057;
  $grey:#d3dce6;

  .text-info{ color:$blue }
  .text-success{ color:$sucess }
  .text-warning{ color:$warning }
  .text-danger{ color:$danger }
  .display-inline-block{
    display: inline-block
  }
  html , body , #app {
    height: 100%;
    width: 100%;
    min-width: 1280px;
    font-size: 12px;
    background-color: #424242;
    color: #2a2a2a;
  }

  [class^="el-icon-Mark"], [class*="el-icon-Mark"] {
    font-family:"iconfont" !important;
    font-size:16px;
    font-style:normal;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
  }
  a{
    text-decoration:none;
  }
  *{
    box-sizing: border-box;
  }
  *{
    margin:0;
    padding:0;
  }
  .pull-left{
    float: left;
  }
  .pull-right{
    float: right;
  }
  .clearfix{
    clear: both;
  }

  .margin-right-10{
    margin-right: 10px;
  }
  .pointer{
    cursor: pointer;
  }
  .fill-height{
    height: 100%;
  }
  
// 无边框窗口设置
  // body{
  //   -webkit-app-region: drag
  // }
  // button {
  //   -webkit-app-region: no-drag;
  // }
</style>
