package com.softwriters.restaurant_reviews;

import java.util.List;

import org.springframework.data.repository.CrudRepository;
import org.springframework.data.repository.query.Param;
import org.springframework.data.rest.core.annotation.RepositoryRestResource;

@RepositoryRestResource
public interface RestaurantsRestRepository extends CrudRepository<Restaurant,Long>{
	
	List<Restaurant> findByCity(@Param("city") String city);
}
