<template lang="pug">
  .users.right-side
    section.content-header
      h1  用户列表
        small 用户
      ol.breadcrumb
        li
          router-link(to="/admin")
            i.fa.fa-dashboard
            span 管理中心
        li
          router-link(to="/admin/users")
            span 用户列表
        li.active 用户列表
    section.content
      .row
        .col-md-12
          .box
            .box-header
              h3.box-title
              router-link.btn.btn-default.pull-right(to="/admin/addUser") 添加用户
            .box-body
              table.table.table-bordered.table-hover
                thead
                  tr
                    th(style="width: 10px") #
                    th  用户名
                    th  用户状态
                    th  用户组
                    th  工作量
                    th(style="width: 20%") 操作
                tbody
                  tr(v-for="(item ,index) in users")
                    td {{ index+1 }}
                    td {{ item.name }}
                    td {{ item.status == 'inuse' ? '已启用' :'已禁用' }}
                    td {{ item.roletype }}
                    td {{ item.taskNum != -1 ? item.taskNum :'查询错误' }}
                    td
                      button.btn(:class="item.status == 'inuse' ? 'btn-danger' :'btn-primary'" @click="editUser(item)") {{ item.status == 'inuse' ? '禁用' :'启用' }}
                      button.btn.btn-defaul(:disabled="item.status == 'inuse' ? false :true" @click="removeTask(item)") 清除任务
            .box-footer.clearfix
</template>

<script>
  import axios from 'axios'
  export default {
    name: 'Users',
    data () {
      return {
        msg: 'Welcome to Users',
        users: [],
        token:''
      }
    },
    methods: {
      editUser: function(item){
        var that = this
        var newStatus = item.status == 'inuse' ? 'notuse' : 'inuse'
        axios.post('/api/editUser' ,JSON.stringify({
          token:that.token,
          name:item.name,
          status: newStatus
        })).then(function(d){
          console.log(d)
          if(d.data.code == 1){
//            item = d.data.data
            item.status = newStatus
//            console.log('editUser',item)
          }
        })
      },
      removeTask: function(user){
        var that = this
        axios.post('/api/clearUserOwn' ,JSON.stringify({
          token:that.token,
          name:user.name
        })).then(function(d){
          if(d.data.code ==1){
            window.alert('清除任务成功')
          }
        })
      }
    },
    beforeMount: function(){
      var that = this
      var user = JSON.parse(this.$cookie.get('user'))
//      console.log('beforeMount:', axios)
      this.token = user.token
      axios.post('/api/userList' ,JSON.stringify({
        token:user.token
      })).then(function(d){
        console.log(d)
        if(d.data.code == 1){
          that.users = d.data.data.users
        }
      })

    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
