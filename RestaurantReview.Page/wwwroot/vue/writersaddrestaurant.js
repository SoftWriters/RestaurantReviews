(function () {
	'use strict';

	Vue.config.devtools = true;
	Vue.use(VueRouter);

	const router = new VueRouter({
		mode: 'history'
	});

	const writers = new Vue({
		router: router,
		data: {
			city: 'Pittsburgh',
			restaurant_name: '',
			restaurant_phone: '',
			address: ''
		},
		created() {
			this.city = document.title.replace('Restaurant - ', '');
		},
		methods: {
			addRestaurant: function (city) {
				//let newRestaurant = document.getElementById('addrestaurant');
				//let formData = new FormData(newRestaurant); // why it's empty?
				let obj = {
					restaurant_name: this.restaurant_name,
					restaurant_phone: this.restaurant_phone,
					address: this.address,
					city: this.city,
					restaurant_id: Math.ceil(Math.random() * 10000)
				}
				axios.post('/api/Restaurants/Add/' + this.city, obj).then((response => {
					var message = response.data == 200 ? 'Submitted' : response.data;
					alert(message);
					this.btnSubmit = 'Submitted';
					this.btnDisable = true;
				}))
					.catch(function (error) {
						alert(error);
					});
			}
		}
	}).$mount('#addrestaurant');
})();