<template lang="pug">
  div.addBucket.right-side
    section.content-header
      h1 Buckets添加
      ol.breadcrumb
        li
          router-link(to="/admin")
            i.fa.fa-dashboard
            span 管理中心
        li
          router-link(to="/admin/bucket") Buckets
        li.active Buckets添加
    section.content
      .row
        .col-md-12
          .box
              .box-header
                h3.box-title
                router-link(to="/admin/addBucket") Buckets添加
              .box-body
                .form-horizontal
                  .form-group
                    label(for="inputSubject") Bucket名称
                    input.form-control#inputSubject(type="text" name="title" placeholder="标题")
                  .form-group
                    label(for="bucketKey") bucketKey
                    input.form-control#bucketKey(type="text" name="bucketKey")
                  .form-group
                    label(for="buckettypes") buckettypes
                    input.form-control#buckettypes(type="text" name="types")
                  .form-group
                    label(for="fluck") needFluck
                    input.form-control#fluck(type="checkbox" name="needFluck")
                  .box-footer.clearfix
                    button.btn.btn-primary(type="submit" name="submit" value="publish") 发布
                    button.btn.btn-info(type="submit" name="submit" value="save") 仅保存
</template>

<script>
  export default {
    name: 'AddBucket',
    data () {
      return {
        msg: 'Welcome to AddBucket'
      }
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
