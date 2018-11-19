#!/bin/bash

curl --header "Content-Type: application/json" \
     --request POST \
     --url http://localhost:6262/api/restaurants \
  --data '{
        "id": "14f86c63-f239-40de-b262-cb8f52edd5f8",
        "name": "Miss Liberty Coffee Shop",
        "url": "https://MissLibertyCoffeeShop-Akron.com",
        "category": "Salvadoran",
        "address1": "9 Spaight Road",
        "address2": "",
        "address3": "",
        "city": "Akron",
        "state": "Ohio",
        "zipCode": "44305",
        "phone": "330-793-1477",
        "price": "4",
        "rating": 2
         }'
curl --header "Content-Type: application/json" \
     --request POST \
     --url http://localhost:6262/api/restaurants \
  --data '{
        "id": "9d4ca15e-2f91-4ff2-b905-ed5760ae1400",
        "name": "The Eclipse",
        "url": "https://TheEclipse-Canton.com",
        "category": "New Canadian",
        "address1": "1 Comanche Drive",
        "address2": "",
        "address3": "",
        "city": "Canton",
        "state": "Ohio",
        "zipCode": "44710",
        "phone": "330-438-2776",
        "price": "1",
        "rating": 2
         }'
curl --header "Content-Type: application/json" \
     --request POST \
     --url http://localhost:6262/api/restaurants \
  --data '{
        "id": "0ffc7ca4-23c2-4571-ac91-44e46ff6badc",
        "name": "Cowboy Chicken",
        "url": "https://CowboyChicken-Charleston.com",
        "category": "Delis",
        "address1": "17 Dryden Court",
        "address2": "",
        "address3": "",
        "city": "Charleston",
        "state": "West Virginia",
        "zipCode": "25305",
        "phone": "304-123-8588",
        "price": "4",
        "rating": 4
         }'
curl --header "Content-Type: application/json" \
     --request POST \
     --url http://localhost:6262/api/restaurants \
  --data '{
        "id": "b6d4eb16-ed5b-453b-8c2e-418ae3907768",
        "name": "The Juniper Lantern",
        "url": "https://TheJuniperLantern-Cincinnati.com",
        "category": "Pop-Up Restaurants",
        "address1": "063 Mifflin Point",
        "address2": "",
        "address3": "",
        "city": "Cincinnati",
        "state": "Ohio",
        "zipCode": "45243",
        "phone": "513-440-2734",
        "price": "4",
        "rating": 2
         }'
curl --header "Content-Type: application/json" \
     --request POST \
     --url http://localhost:6262/api/restaurants \
  --data '{
        "id": "307642a7-ca1c-472a-b069-c6f11dc4d8e9",
        "name": "The Greek Demon",
        "url": "https://TheGreekDemon-Cleveland.com",
        "category": "Burgers",
        "address1": "674 Harper Center",
        "address2": "",
        "address3": "",
        "city": "Cleveland",
        "state": "Ohio",
        "zipCode": "44197",
        "phone": "216-759-6687",
        "price": "4",
        "rating": 1
         }'
curl --header "Content-Type: application/json" \
     --request POST \
     --url http://localhost:6262/api/restaurants \
  --data '{
        "id": "4453b4d1-9619-421b-857c-e778d6d8a2ad",
        "name": "The Moroccan Word",
        "url": "https://TheMoroccanWord-Dayton.com",
        "category": "Falafel",
        "address1": "6 Bowman Road",
        "address2": "",
        "address3": "",
        "city": "Dayton",
        "state": "Ohio",
        "zipCode": "45470",
        "phone": "937-583-6781",
        "price": "5",
        "rating": 5
         }'
curl --header "Content-Type: application/json" \
     --request POST \
     --url http://localhost:6262/api/restaurants \
  --data '{
        "id": "060f50d1-6bc8-4b1d-b0cb-fabd808b5fc3",
        "name": "Embers",
        "url": "https://Embers-Erie.com",
        "category": "Spanish",
        "address1": "4 Namekagon Point",
        "address2": "",
        "address3": "",
        "city": "Erie",
        "state": "Pennsylvania",
        "zipCode": "16522",
        "phone": "814-581-3819",
        "price": "2",
        "rating": 3
         }'
curl --header "Content-Type: application/json" \
     --request POST \
     --url http://localhost:6262/api/restaurants \
  --data '{
         "id": "2134f32f-787c-4596-8b38-5051f6893cd8",
         "name": "Elizabeths Cafe & Winery",
         "url": "https://ElizabethsCafe&Winery-Hamilton.com",
         "category": "Tacos",
         "address1": "17 Debra Hill",
         "address2": "",
         "address3": "",
         "city": "Hamilton",
         "state": "Ohio",
         "zipCode": "45020",
         "phone": "937-352-5977",
         "price": "3",
         "rating": 5
         }'
curl --header "Content-Type: application/json" \
     --request POST \
     --url http://localhost:6262/api/restaurants \
  --data '{
        "id": "8f000a15-d69a-4e15-83be-db5eb25b0675",
        "name": "The Wall",
        "url": "https://TheWall-Huntington.com",
        "category": "Vegan",
        "address1": "82599 Jenifer Junction",
        "address2": "",
        "address3": "",
        "city": "Huntington",
        "state": "West Virginia",
        "zipCode": "25709",
        "phone": "304-226-2881",
        "price": "3",
        "rating": 3
         }'
curl --header "Content-Type: application/json" \
     --request POST \
     --url http://localhost:6262/api/restaurants \
  --data '{
        "id": "f5440e9e-2086-4821-9f96-b7e90b41b081",
        "name": "The Urban Cottage",
        "url": "https://TheUrbanCottage-Morgantown.com",
        "category": "Australian",
        "address1": "8 Golden Leaf Junction",
        "address2": "",
        "address3": "",
        "city": "Morgantown",
        "state": "West Virginia",
        "zipCode": "26505",
        "phone": "304-454-4238",
        "price": "4",
        "rating": 4
         }'
curl --header "Content-Type: application/json" \
     --request POST \
     --url http://localhost:6262/api/restaurants \
  --data '{
        "id": "1f1d514a-eecd-49a2-8e9b-a523abc670eb",
         "name": "Sicily Pizza",
         "url": "https://SicilyPizza-Pittsburgh.com",
         "category": "Sicilian",
         "address1": "031 Bonner Junction",
         "address2": "Corben Crossing Mall",
         "address3": "",
         "city": "Akron",
         "state": "Ohio",
         "zipCode": "44305",
         "phone": "267-477-6939",
         "price": "3",
         "rating": 2
         }'
curl --header "Content-Type: application/json" \
     --request POST \
     --url http://localhost:6262/api/restaurants \
  --data '{
        "id": "9221b277-535e-4d92-8885-522fda3ede30",
        "name": "Earls Rib Palace",
        "url": "https://EarlsRibPalace-Pittsburgh.com",
        "category": "American",
        "address1": "22 Gina Point",
        "address2": "Suite 40663 ",
        "address3": "",
        "city": "Pittsburgh",
        "state": "Pennsylvania",
        "zipCode": "15250",
        "phone": "412-963-8816",
        "price": "3",
        "rating": 3
         }'
curl --header "Content-Type: application/json" \
     --request POST \
     --url http://localhost:6262/api/restaurants \
  --data '{
        "id": "beaadfc9-c495-4a87-a2ea-1f58383805f4",
        "name": "The Winter Moments",
        "url": "https://TheWinterMoments-Midland.com",
        "category": "Tapas Bars",
        "address1": "853 Sherman Junction",
        "address2": "",
        "address3": "",
        "city": "Pittsburgh",
        "state": "Pennsylvania",
        "zipCode": "15219",
        "phone": "412-963-2003",
        "price": "2",
        "rating": 4
         }'
curl --header "Content-Type: application/json" \
     --request POST \
     --url http://localhost:6262/api/restaurants \
  --data '{
        "id": "bb8d78d3-b9f4-40c3-98e3-5ac23458d37a",
         "name": "Luce Restaurant",
         "url": "https://LuceRestaurant-Pittsburgh.com",
         "category": "Teppanyaki",
         "address1": "0 Mcbride Lane",
         "address2": "Building 182 ",
         "address3": "SE",
         "city": "Pittsburgh",
         "state": "Pennsylvania",
         "zipCode": "15219",
         "phone": "262-726-6658",
         "price": "3",
         "rating": 1
         }'
curl --header "Content-Type: application/json" \
     --request POST \
     --url http://localhost:6262/api/restaurants \
  --data '{
        "id": "e3136824-78bb-493e-8431-296626a48251",
        "name": "The New Garden",
        "url": "https://TheNewGarden-Pittsburgh.com",
        "category": "Hainan",
        "address1": "323 Southridge Parkway",
        "address2": "",
        "address3": "",
        "city": "Pittsburgh",
        "state": "Pennsylvania",
        "zipCode": "15234",
        "phone": "831-146-0239",
        "price": "5",
        "rating": 5
         }'
curl --header "Content-Type: application/json" \
     --request POST \
     --url http://localhost:6262/api/restaurants \
  --data '{
        "id": "aac2d5e7-5876-45ed-85dd-99e80e08e348",
        "name": "The Wild Palace",
        "url": "https://TheWildPalace-Pittsburgh.com",
        "category": "Burmese",
        "address1": "13434 Raven Avenue",
        "address2": "",
        "address3": "",
        "city": "Pittsburgh",
        "state": "Pennsylvania",
        "zipCode": "15243",
        "phone": "417-279-0845",
        "price": "5",
        "rating": 3
         }'
curl --header "Content-Type: application/json" \
     --request POST \
     --url http://localhost:6262/api/restaurants \
  --data '{
        "id": "b7493a2e-e80d-4a09-b0f6-147ddf1878f8",
        "name": "LA Bamba Mexican Restaurant",
        "url": "https://LABambaMexicanRestaurant-Pittsburgh.com",
        "category": "Mexican",
        "address1": "377 Ilene Court",
        "address2": "",
        "address3": "",
        "city": "Pittsburgh",
        "state": "Pennsylvania",
        "zipCode": "15228",
        "phone": "512-515-3994",
        "price": "5",
        "rating": 2
         }'
curl --header "Content-Type: application/json" \
     --request POST \
     --url http://localhost:6262/api/restaurants \
  --data '{
        "id": "0dc796e8-63c7-4548-8368-b0f6a61f9b95",
        "name": "Benkei Japanese Restaurant",
        "url": "https://BenkeiJapaneseRestaurant-Pittsburgh.com",
        "category": "Vegetarian",
        "address1": "42 Westridge Drive",
        "address2": "",
        "address3": "",
        "city": "Pittsburgh",
        "state": "Pennsylvania",
        "zipCode": "15136",
        "phone": "805-574-3646",
        "price": "3",
        "rating": 1
         }'
curl --header "Content-Type: application/json" \
     --request POST \
     --url http://localhost:6262/api/restaurants \
  --data '{
        "id": "85ff7c09-e351-48ec-b755-82b16b4715ab",
        "name": "Denicolas Restaurant",
        "url": "https://DenicolasRestaurant-Pittsburgh.com",
        "category": "Halal",
        "address1": "09459 Moland Road",
        "address2": "",
        "address3": "",
        "city": "Pittsburgh",
        "state": "Pennsylvania",
        "zipCode": "15250",
        "phone": "540-545-2328",
        "price": "2",
        "rating": 1
         }'
curl --header "Content-Type: application/json" \
     --request POST \
     --url http://localhost:6262/api/restaurants \
  --data '{
        "id": "cddd4585-593b-4a32-9c19-6d21dc5fbda8",
        "name": "The Bitter Faire",
        "url": "https://TheBitterFaire-Pittsburgh.com",
        "category": "Falafel",
        "address1": "04 Moland Hill",
        "address2": "",
        "address3": "",
        "city": "Pittsburgh",
        "state": "Pennsylvania",
        "zipCode": "15205",
        "phone": "918-741-9636",
        "price": "3",
        "rating": 4
         }'
