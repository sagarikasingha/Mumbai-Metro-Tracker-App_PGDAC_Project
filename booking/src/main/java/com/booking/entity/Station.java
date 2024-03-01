package com.booking.entity;

import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.JoinColumn;
import jakarta.persistence.ManyToOne;
import jakarta.persistence.Table;

@Entity
@Table(name = "stations")
public class Station {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int stationId;

    private String stationName;

    @ManyToOne
    @JoinColumn(name = "route_id")
    private RouteLine routeLine;

	public Station() {
		super();
	}

	public Station(int stationId, String stationName, RouteLine routeLine) {
		super();
		this.stationId = stationId;
		this.stationName = stationName;
		this.routeLine = routeLine;
	}

	public int getStationId() {
		return stationId;
	}

	public void setStationId(int stationId) {
		this.stationId = stationId;
	}

	public String getStationName() {
		return stationName;
	}

	public void setStationName(String stationName) {
		this.stationName = stationName;
	}

	public RouteLine getRouteLine() {
		return routeLine;
	}

	public void setRouteLine(RouteLine routeLine) {
		this.routeLine = routeLine;
	}

	@Override
	public String toString() {
		return "Station [stationId=" + stationId + ", stationName=" + stationName + ", routeLine=" + routeLine + "]";
	}


}
