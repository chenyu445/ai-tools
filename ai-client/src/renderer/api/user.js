import fetch from './fetch'

const userService = {}

userService.login = function (params) {
  return fetch({
    url: 'login',
    method: 'post',
    data: params
  })
}

export default userService
