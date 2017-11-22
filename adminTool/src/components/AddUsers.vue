<template lang="pug">
  div.addUsers.right-side
    section.content-header
      h1 用户添加
      ol.breadcrumb
        li
          router-link(to="/admin")
            i.fa.fa-dashboard
            span 管理中心
        li
          router-link(to="/admin/users") 用户列表
        li.active 用户添加
    section.content
      .row
        .col-md-12
          .box
            div
              .box-header
                h3.box-title
                router-link(to="/admin/addUser") 用户添加
              .box-body
                .form-horizontal
                  .form-group
                    label.col-sm-2.control-label(for="inputSubject") 用户名称
                    .col-sm-10
                      input#inputSubject.form-control(type="text" name="title" placeholder="用户名称" v-model="user.name")
                  .form-group
                    label.col-sm-2.control-label(for="userpwd" class="") 用户密码
                    .col-sm-10
                      input.form-control#userpwd(type="text" name="title" placeholder="用户密码" v-model="user.passwd")
                  .form-group
                    label.col-sm-2.control-label(for="usetypes") 用户类型
                    .col-sm-10
                      select.form-control#usetypes(type="text" name="types" v-model="user.roletype")
                        option(disabled value="") 请选择
                        option(v-for="rol in role" :value="rol.type") {{rol.type}}
                  .form-group
                    .col-sm-offset-2.col-sm-10
                      .checkbox
                        label(for="userKey")
                          input(type="checkbox" name="userKey" id="userKey" v-model="user.preStatus")
                          span 启用
                  .box-footer.clearfix
                    .col-sm-offset-2.col-sm-10
                      button.btn.btn-primary( @click="submit") 提交
</template>

<script>
  import axios from 'axios'
  export default {
    name: 'AddUser',
    data () {
      return {
        msg: 'Welcome to Add Users',
        user:{
          name: '',
          passwd: '',
          preStatus: true,
//          status: 'inuse',
          roletype: 'annotater'
        },
        role:[
          { code:1,type:'annotater'},
          { code:2,type:'expert'},
          { code:3,type:'webadmin'}
        ]
      }
    },
    methods:{
      submit:function(){
        var that = this
        that.user.status = that.user.preStatus ? 'inuse' : 'notuse'
        that.user.token = JSON.parse(that.$cookie.get('user')).token
        axios.post('/api/createUser',JSON.stringify(that.user))
          .then(function(msg){
            if(msg.data.code == 1 ){
              window.alert('添加成功')
              setTimeout(function(){
                window.location.href = '#/admin/users'
              },2000)

            }
          })
      }
    }

  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
