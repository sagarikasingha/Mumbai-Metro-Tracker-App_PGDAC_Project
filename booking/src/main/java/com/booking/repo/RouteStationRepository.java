package com.booking.repo;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import com.booking.entity.RouteStation;

@Repository
public interface RouteStationRepository extends JpaRepository<RouteStation, Integer> {
	
	 RouteStation findFirstByStationFromStationIdAndStationToStationId(int stationIdFrom, int stationIdTo);
}