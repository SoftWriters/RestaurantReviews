(function () {
	'use strict';

	Vue.config.devtools = true;
	Vue.use(VueRouter);

	const router = new VueRouter({
		mode: 'history'
	});

	const restaurant = new Vue({
		router: router,
		data: {
			city: 'Pittsburgh',
			restaurants: [],
			reviews: [],
			sortKey: 'restaurant_id',
			sortOrder: 1,
			page: {
				currentPage: 1
			},
			pageSize: 20,
			message: 'Loading... If this message persists check your API connection'
		},
		created() {
			this.city = document.title.replace('Restaurant - ', '');
			this.getRestaurants(this.city);
		},
		methods: {
			sort: function (s) {
				if (s === this.sortKey) {
					this.sortOrder = -1 * this.sortOrder;
				}
				this.sortKey = s;
				this.page.currentPage = 1;
			},
			getRestaurants: function (city) {
				axios.get('/api/Restaurants/' + city).then((response) => {
					this.restaurants = response.data;
				})
					.catch(function (error) {
						alert(error);
					});
			},
			review: function (restaurant_id) {
				window.location.href = "/Review/" + restaurant_id;
				//axios.get('/api/review/' + city).then((response) => {
				//	this.reviews = response.data;
				//})
				//router.push({
				//	path: '/Detail'
				//});
			}
		}
	}).$mount('#writersrestaurant');

	Vue.filter('dateTime', function (value) {
		if (value) {
			return moment(String(value)).format('MM/DD/YYYY');
		}
	});

	Vue.directive('arrow', {
		bind: function (el, binding) {
			el.style.color = 'dodgerblue';

			if (restaurant.sortKey === binding.value.sortKey) {
				if (restaurant.sortOrder === 1) {
					el.innerHTML = '▴';
				} else {
					el.innerHTML = '▾';
				}
			} else {
				el.innerHTML = '';
			}
		},
		update: function (el, binding) {
			if (restaurant.sortKey === binding.value.sortKey) {
				if (restaurant.sortOrder === 1) {
					el.innerHTML = '▴';
				} else {
					el.innerHTML = '▾';
				}
			} else {
				el.innerHTML = '';
			}
		}
	});
})();