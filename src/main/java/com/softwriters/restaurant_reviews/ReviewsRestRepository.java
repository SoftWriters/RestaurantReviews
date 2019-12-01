package com.softwriters.restaurant_reviews;

import java.util.List;

import org.springframework.data.repository.CrudRepository;
import org.springframework.data.repository.query.Param;
import org.springframework.data.rest.core.annotation.RepositoryRestResource;

@RepositoryRestResource
public interface ReviewsRestRepository extends CrudRepository<Review,Long>{

	List<Review> findByUser(@Param("user") String user);
}
