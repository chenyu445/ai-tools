<template lang="pug">
	.login
		.user(:class="{setconfig: hasConfig}")
			.logo 
				span Login	
				i.pull-right.el-icon-circle-close
			el-form(:model="form" :rules="rules")
				el-form-item(prop="userName" label="Username:")
					el-input(placeholder="userName",type="text",v-model.trim.lazy="form.userName" size="mini")
				el-form-item(prop="passWord" label="Password:")
					el-input(placeholder="Password",type="password",v-model.trim.lazy="form.passWord" size="mini")
				el-button.submit(type="primary" @click="onSubmit" size="mini" ) Sign in
			.showConfig
				button.pointer(@click="showConfig(hasConfig)") 
					i.el-icon-arrow-down
			.config
				el-form(:model="hostConfig" :rules="rules")
					el-form-item(prop="host" label="Host:")
						el-input(placeholder="http://",v-model.trim.lazy="hostConfig.host" size="mini")
					el-form-item(prop="port" label="Port:")
						el-input(placeholder="port",type="number",v-model.trim.lazy="hostConfig.port" size="mini")
</template>

<script>
import SHA256 from 'js-sha256'
import userService from "@/api/user"
import { Message } from 'element-ui'
import router from '@/router'
import Cookies from 'js-cookie'

export default {
  name: 'login',
  components: {
  },
  data () {
    return {
      form:{
				userName :'',
				passWord :''
			}
			,rules:{
				userName:[
					{ required: true, message: 'please input userName', trigger: 'blur' }
				]
				,passWord:[
					{ required: true, message: 'please input password', trigger: 'blur' }
				]
				,host:[
					{ required: true, message: 'please input password', trigger: 'blur' }
				]
				,port:[
					{ required: true, message: 'please input password', trigger: 'blur' }
				]
			},
			hostConfig:{
				host:'',
				port: ''
			}
			,hasConfig: !Cookies.get('apiConfig')
    }
  },
  computed: {

  },
  methods: {
    onSubmit () {
			Cookies.set('apiConfig',this.hostConfig)
			var user = {}
			user.passwd = SHA256(this.form.passWord)
			user.name = this.form.userName
		
			userService.login(user)
				.then(function(res){
					Cookies.set('token',res.token)					
					router.push('index')
				})
				.catch(function(error){
					console.log('login:', error)
					Message({
						message: error,
						type: 'error',
						duration: 5 * 1000
					})
				})
    },
		showConfig (hasConfig) {
			this.hasConfig = !hasConfig
		}
	},
	mounted () {
		if(Cookies.get('apiConfig')) {this.hostConfig = JSON.parse(Cookies.get('apiConfig'))} 
		// console.log('mounted:',this.hostConfig)
	}
}
</script>

<style rel="stylesheet/scss" lang="scss">
	.login{
		height: 100%;
		position: relative;
		background-color: #383838;
		color: #fff;		
		.user{
			background-color: #4d4d4d;
			width:400px;
			height: 280px;
			border-radius: 10px;
			position: absolute;
			left:50%;
			top: 50%;
			margin-left: -200px;
			margin-top: -220px;
			overflow: hidden;
			transition: all 0.5s;
			.logo{
				// background-color: #1eb4e1;
				padding: 10px 20px;	
				border-bottom:2px solid #2a2a2a;			
			}
			.el-form-item{
				margin-bottom: 10px;
			}
			.el-form-item__label{
				line-height: 20px;
			}
			.el-form-item__label{
				color: #fff
			}
			.el-form{
				padding:10px 14px;
				> button{
					margin-top:20px;
					background-color: #52bacf;
				}
				.submit{
					display: block;
					width: 100%;
					// margin-top: 10px;
				}
			}
			.showConfig {
				text-align: center;
				button{
					display: inline-block;
					width: 40px;
					height: 40px;
					text-align: center;
					background-color: transparent;
					outline: none;
					border: none;
				}
			}
			&.setconfig{
				height: 440px;
			}
		}
	}
</style>
