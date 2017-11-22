import Vue from 'vue'
import Router from 'vue-router'
const Login = () => import('../view/login/login')
const Index = () => import('../view/index/index')

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'open',
      redirect: '/login'
    },
    {
      path: '/login',
      name: 'login',
      component: Login
    },
    {
      path: '/index',
      name: 'index',
      component: Index
    },
    {
      path: '*',
      redirect: '/'
    }
  ]
})