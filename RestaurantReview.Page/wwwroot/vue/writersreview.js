(function () {
	'use strict';

	Vue.config.devtools = true;
	Vue.use(VueRouter);

	const router = new VueRouter({
		mode: 'history'
	});

	const rev = new Vue({
		router: router,
		data: {
			restaurant_id: '',
			//restaurant_name: '',
			user_id: '',
			reviews: [],
			sortKey: 'restaurant_name',
			sortOrder: 1,
			page: {
				currentPage: 1
			},
			pageSize: 15,
			message: 'Loading... If this message persists check your API connection'
		},
		mounted() {
			//this.restaurant_id = this.$route.params.id;
			this.restaurant_id = document.title.replace('Restaurant - ', '');

			axios.get('/api/Reviews/' + this.restaurant_id).then((response) => {
				this.reviews = response.data;
			})
				.catch(function (error) {
					alert(error);
				});
		},
		methods: {
			sort: function (s) {
				if (s === this.sortKey) {
					this.sortOrder = -1 * this.sortOrder;
				}
				this.sortKey = s;
				this.page.currentPage = 1;
			},
			getReviews: function (restaurant_id) {
				this.restaurant_id = restaurant_id;
				axios.get('/api/Reviews/' + restaurant_id).then((response) => {
					this.reviews = response.data;
				})
					.catch(function (error) {
						alert(error);
					});
			}
		}
	}).$mount('#writersreview');

	Vue.filter('dateTime', function (value) {
		if (value) {
			return moment(String(value)).format('MM/DD/YYYY');
		}
	});

	Vue.directive('arrow', {
		bind: function (el, binding) {
			el.style.color = 'dodgerblue';

			if (rev.sortKey === binding.value.sortKey) {
				if (rev.sortOrder === 1) {
					el.innerHTML = '▴';
				} else {
					el.innerHTML = '▾';
				}
			} else {
				el.innerHTML = '';
			}
		},
		update: function (el, binding) {
			if (rev.sortKey === binding.value.sortKey) {
				if (rev.sortOrder === 1) {
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