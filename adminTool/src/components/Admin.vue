<template lang="pug">
  div
    header.header
      nav.navbar.navbar-static-top(role="navigation" )
        a .navbar-btn.sidebar-toggle(data-toggle="offcanvas" role="button")
          span.sr-only.Toggle.navigation
          span.icon-bar
          span.icon-bar
          span.icon-bar
        div.navbar-right
          ul.nav.navbar-nav
            li.dropdown.user.user-menu
              a.dropdown-toggle(data-toggle="dropdown")
                i.glyphicon.glyphicon-user
                span {{user.name}}
                  i.caret
              ul.dropdown-menu
                li.user-header.bg-light-blue
                  img.img-circle(src="/static/img/avatar04.png" alt="User Image")
                  p  Beyondsoft - {{user.name}}
                    small 登陆于2016年01月01日
                li.user-footer
                  .pull-left
                    a.btn.btn-default.btn-flat 用户信息
                  .pull-right
                    a.btn.btn-default.btn-flat(@click="logout()") 注销登陆
    .wrapper.row-offcanvas.row-offcanvas-left
      aside.left-side.sidebar-offcanvas
        section.sidebar
          .user-panel
            .pull-left.image
              img.img-circle(src="/static/img/avatar04.png" alt="User Image" )
            .pull-left.info
              p Hello, {{user.name}}
              a
                i.fa.fa-circle.text-success
                span 在线
          //- <form action="#" method="get" class="sidebar-form">
          //-   <div class="input-group">
          //-     <input type="text" name="q" class="form-control" placeholder="操作"/>
          //-     <span class="input-group-btn">
          //-         <button type='submit' name='search' id='search-btn' class="btn btn-flat"><i class="fa fa-search"></i></button>
          //-     </span>
          //-   </div>
          //- </form>
          ul.sidebar-menu
            li.active
              router-link( to="/admin")
                i.fa.fa-dashboard
                span 控制台
            li.treeview
              a
                i.fa.fa-th-large
                span 用户管理
                i.fa.fa-angle-left.pull-right
              ul.treeview-menu
                li
                  router-link(to="/admin/users")
                    i.fa.fa-list
                    span 用户列表
                li
                  router-link(to="/admin/addUser")
                    i.fa.fa-edit
                    span 用户添加
            li.treeview
              a
                i.fa.fa-th-large
                span Bucket管理
                i.fa.fa-angle-left.pull-right
              ul.treeview-menu
                li
                  router-link(to="/admin/bucket")
                    i.fa.fa-list
                    span Bucket列表
                //- li
                  router-link(to="/admin/addBucket")
                    i.fa.fa-list
                    span Bucket添加
      router-view
      section.content-footer
        div.text-center  © 2016 All Rights Reserved. Beyondsoft
</template>

<script>
  import axios from 'axios'
export default {
  name: 'Admin',
  data () {
    return {
      user: {
        name: '',
        loginTime: null
      },
      token: ''
    }
  },
  methods: {
    logout: function () {
      debugger
      var that = this
      that.$cookie.delete('user')
      window.location.href = '#/login'
//      axios.post('/api/logout', JSON.stringify({token: that.token}))
//        .then(function (msg) {
//          that.$cookie.delete('user')
//        })
//        .catch(function (err) {
//          console.log('logout', err)
//        })
    }
  },
  beforeCreate: function () {
    var that = this
    var user = JSON.parse(that.$cookie.get('user'))
//    console.log('admin beforeCreate',user)

  },
  beforeMount: function () {
    var that = this
    var user = JSON.parse(that.$cookie.get('user'))
    console.log('admin beforeMount', user)

//    console.log(user.token)
//    debugger
    if (user && user.token ) {
      that.token = user.token
      that.user.name = user.name
      that.user.loginTime = new Date(user.name)
    } else {
      window.location.href = '#/login'
    }

  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
