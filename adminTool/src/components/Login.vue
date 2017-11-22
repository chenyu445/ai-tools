<template lang="pug">
  .signinpanel
    .row
      .col-sm-6.col-sm-offset-3
        div
          h4.no-margins(style="font-weight:bold; font-size:24px;") 管理员登录
          input(type="text" class="form-control uname" placeholder="用户名" v-model="user.name")
          input(type="password" class="form-control pword m-b" placeholder="密码" v-model="user.passwd")
          p {{error}}
          a.forget 忘记密码了？
          button.btn.btn-success.btn-block(@click="login") 登 录
    .signup-footer
      .text-center &copy; 2017 All Rights Reserved. Beyondsoft
</template>

<script>
  import axios from 'axios'
  import SHA256 from 'js-sha256'
export default {
  name: 'Login',
  data () {
    return {
      user: {
        name:'',
        passwd: ''
      },
      error:''
    }
  },
  methods: {
    login: function () {
//      debugger;
      var that = this
      that.user.passwd = SHA256(that.user.passwd)
      axios.post('/api/login',JSON.stringify(that.user))
        .then(function(e){
          // console.log('login',e)
          var res = e.data
          if (res.code == 1) {
            that.$cookie.set('user',JSON.stringify({
              token:res.data.token,
              name:that.user.name,
              loginTime: new Date()
            }))
            window.location.href= "/"
          }else{
            that.error = '账号或密码错误'
          }
        })
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  .btn-success {
    background-color: #1c84c6;
    border-color: #1c84c6;
    color: #FFF
  }
  .btn-success:hover {
    background-color: #1a7bb9;
    border-color: #1a7bb9;
    color: #FFF
  }
  .signinpanel .forget {font-size: 12px;}
</style>
