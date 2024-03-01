package com.booking.repo;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import com.booking.entity.Station;

@Repository
public interface StationRepository extends JpaRepository<Station, Integer> {
	
}