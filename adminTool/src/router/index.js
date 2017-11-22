import Vue from 'vue'
import Router from 'vue-router'
import Login from '@/components/Login'
import Admin from '@/components/Admin'
import Users from '@/components/Users'
import AddUsers from '@/components/AddUsers'
import Bucket from '@/components/Bucket'
import Hello from '@/components/Hello'
import AddBucket from '@/components/AddBucket'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'Index',
      redirect: {name: 'Admin'},
      meta: {
        requiresAuth: true // 设置当前路由需要校验
      }
    },
    {
      path: '/login',
      name: 'Login',
      component: Login
    },
    {
      path: '/admin',
      name: 'Admin',
      component: Admin,
      redirect: {name: 'Hello'},
      children: [
        {
          path: 'users',
          name: 'Users',
          component: Users
        },
        {
          path: 'hello',
          name: 'Hello',
          component: Hello
        },
        {
          path: 'addUser',
          name: 'AddUsers',
          component: AddUsers
        },
        {
          path: 'bucket',
          name: 'Bucket',
          component: Bucket
        },
        {
          path: 'addBucket',
          name: 'AddBucket',
          component: AddBucket
        }
      ],
      meta: {
        requiresAuth: true // 设置当前路由需要校验
      }
    }
  ]
})
