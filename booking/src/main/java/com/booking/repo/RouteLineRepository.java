package com.booking.repo;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import com.booking.entity.RouteLine;

@Repository
public interface RouteLineRepository extends JpaRepository<RouteLine, Integer> {
	
}
