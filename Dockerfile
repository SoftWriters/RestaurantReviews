FROM node:argon
RUN mkdir /RestaurantReviewApp
WORKDIR /RestaurantReviewApp
COPY package.json /RestaurantReviewApp
RUN npm install
COPY . /RestaurantReviewApp
EXPOSE 8080
CMD ["npm", "run dev"]