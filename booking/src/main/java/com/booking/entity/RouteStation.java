package com.booking.entity;

import java.sql.Time;

import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.JoinColumn;
import jakarta.persistence.ManyToOne;
import jakarta.persistence.Table;

@Entity
@Table(name = "route_stations")
public class RouteStation {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int id;

    @ManyToOne
    @JoinColumn(name = "station_id_from")
    private Station stationFrom;

    @ManyToOne
    @JoinColumn(name = "station_id_to")
    private Station stationTo;

    private int stop;
    private Time time;
    private float fare;
    private int interchangeStops;
    
	public RouteStation() {
		super();
	}

	public RouteStation(int id, Station stationFrom, Station stationTo, int stop, Time time, float fare,
			int interchangeStops) {
		super();
		this.id = id;
		this.stationFrom = stationFrom;
		this.stationTo = stationTo;
		this.stop = stop;
		this.time = time;
		this.fare = fare;
		this.interchangeStops = interchangeStops;
	}

	public int getId() {
		return id;
	}

	public void setId(int id) {
		this.id = id;
	}

	public Station getStationFrom() {
		return stationFrom;
	}

	public void setStationFrom(Station stationFrom) {
		this.stationFrom = stationFrom;
	}

	public Station getStationTo() {
		return stationTo;
	}

	public void setStationTo(Station stationTo) {
		this.stationTo = stationTo;
	}

	public int getStop() {
		return stop;
	}

	public void setStop(int stop) {
		this.stop = stop;
	}

	public Time getTime() {
		return time;
	}

	public void setTime(Time time) {
		this.time = time;
	}

	public float getFare() {
		return fare;
	}

	public void setFare(float fare) {
		this.fare = fare;
	}

	public int getInterchangeStops() {
		return interchangeStops;
	}

	public void setInterchangeStops(int interchangeStops) {
		this.interchangeStops = interchangeStops;
	}

	@Override
	public String toString() {
		return "RouteStation [id=" + id + ", stationFrom=" + stationFrom + ", stationTo=" + stationTo + ", stop=" + stop
				+ ", time=" + time + ", fare=" + fare + ", interchangeStops=" + interchangeStops + "]";
	}

}
