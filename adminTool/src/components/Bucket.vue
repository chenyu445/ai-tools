<template lang="pug">
  .bucket.right-side
    section.content-header
      h1  Buckets列表
        small Buckets
      ol.breadcrumb
        li
          router-link(to="/admin")
            i.fa.fa-dashboard
            span 管理中心
        li
          router-link(to="/admin/bucket")
            span Buckets
        li.active Buckets列表
    section.content
      .row
        .col-md-12
          .box
            .box-header
              h3.box-title
              a.btn.btn-default.pull-right(href="javascript:void(0)" @click="downloadReport") 导出标注进度报告
            .box-body
              table.table.table-bordered.table-hover
                thead
                  tr
                    th(style="width: 10px") #
                    th bucket
                    th 类型
                    th 是否双盲
                    th 标注进度
                    th 已标注
                    th 总量
                    th(style="width: 20%") 操作
                tbody
                  tr(v-for="(item ,index) in buckets")
                    td {{ index+1 }}
                    td {{ item.name }}
                    td
                      button.btn.btn-primary.margin-left-5(v-for="type in item.buckettypes") {{type}}
                    td {{ item.doubleblind == 1 ? '未启用' : '已启用'  }}
                    td {{ item.totalfilenum != 0 ? ((item.annotatedfilenum / item.totalfilenum) * 100 ).toFixed(2)+ "%" : "0%"}}
                    td {{ item.annotatedfilenum != -1 ? item.annotatedfilenum : '查询错误' }}
                    td {{ item.totalfilenum != -1 ? item.totalfilenum : '查询错误' }}
                    td
                      button.btn(:class="item.doubleblind != 1 ? 'btn-danger' :'btn-primary'" @click="editDoubleBlind(item)") {{ item.doubleblind != 1  ? '禁用' :'启用' }}
            .box-footer.clearfix
    iframe.hide#downloadIframe(src="" frameborder="0")
</template>

<script>
  import axios from 'axios'
export default {
  name: 'Bucket',
  data () {
    return {
      msg: 'Welcome to Bucket',
      buckets: [],
      download: ''
    }
  },
  methods:{
    downloadReport:function(){
//      this.download = '/static/js.tar.gz'
      var user = JSON.parse(this.$cookie.get('user'))
      axios.post('/api/exportReport' ,JSON.stringify({
        token:user.token
      })).then(function(d){
        console.log(d)
        if(d.data.code == 1){
          document.getElementById('downloadIframe').src = d.data.data.url
        }
      })
//      document.getElementById('downloadIframe').src = '/static/js.tar.gz'
    },
    editDoubleBlind: function(bucket){
      var user = JSON.parse(this.$cookie.get('user'))
      var newStatus = bucket.doubleblind  == 1 ? 2 : 1
      axios.post('/api/bucketDoubleBlind' ,JSON.stringify({
        token:user.token,
        bucketid:bucket.id,
        status: newStatus
      })).then(function(d){
        console.log(d)
        if(d.data.code == 1){
          bucket.doubleblind = newStatus
        }
      })
    }
  },
  beforeMount: function(){
    var that = this
    var user = JSON.parse(this.$cookie.get('user'))
    //      console.log('beforeMount:', axios)
    axios.post('/api/bucketReport' ,JSON.stringify({
      token:user.token
    })).then(function(d){
      console.log(d)
      if(d.data.code == 1){
        that.buckets = d.data.data.buckets
      }
    })

  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  .margin-left-5{
    margin-left: 5px;
  }
</style>
