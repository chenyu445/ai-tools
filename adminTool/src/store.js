import Vue from 'vue'

export default {
  debug: true,
  state: {
    isLogin: false
  },
  setLoginState : function(newValue){
    if(this.debug){
      console.log('setLoginState:', newValue)
    }
    this.state.isLogin = newValue
  }
}
