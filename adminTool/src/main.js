// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import router from './router'
import Cookie from 'vue-cookie'
// import store from './store'

// import Router from 'vue-router'
Vue.use(Cookie)
Vue.config.productionTip = false

/* eslint-disable no-new */
var app = new Vue({
  el: '#app',
  router,
  template: '<App/>',
  components: { App }
})

router.beforeEach((to, from, next) => {
  // console.log(store)
  // console.log(Cookie)
  console.log(app.store)
  // console.log('beforeEach:', to)
  var user = JSON.parse(Cookie.get('user'))
  console.log(user)
  if (to.matched.some(record => record.meta.requiresAuth)) {
    if(user && user.token){
      next()
    }else{
      next({path:'/login'})
    }
  }else{
    next()
  }
  // if (to.matched.some(record => record.meta.requiresAuth)) {
  //   if (store.state.isLogin) {
  //     next()
  //   } else {
  //     next({path: '/login'})
  //   }
  // } else {
  //   next()
  // }
})
