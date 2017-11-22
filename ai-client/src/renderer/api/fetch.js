import axios from 'axios'
import Cookies from 'js-cookie'
import { Message } from 'element-ui'

// 创建axios实例
const service = axios.create({  
  timeout: 10000                  // 请求超时时间
})

// request拦截器
service.interceptors.request.use(config => {
  var token = Cookies.get('token')
  var apiConfig =JSON.parse(Cookies.get('apiConfig')) 
  config.url = apiConfig && apiConfig.host ? apiConfig.host+':'+apiConfig.port+'/api/'+config.url: '/api/'+config.url;// api的base_url
  if (token) {
    config.transformRequest = function(data,header){
      data.token = token
      return JSON.stringify(data)
    }
  }
  return config
}, error => {
  // Do something with request error
  console.log(error) // for debug
  Promise.reject(error)
})

// respone拦截器
service.interceptors.response.use(
  response => {    
    if (response.data.code === 1) {
      return response.data.data
    } else {
      return Promise.reject(response.data.msg)
    }
  },
  error => {
    console.log('err:' , error)// for debug
    Message({
      showClose: true,
      message: error.message,
      type: 'error',
      duration: 5 * 1000
    })
    return Promise.reject(error)
  }
)

export default service
