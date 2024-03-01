package com.booking.entity;


import java.time.LocalTime;

import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;

@Entity
@Table(name = "tickets")
public class Ticket {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int ticketId;

    private int stationIdFrom;
    private int stationIdTo;
    private float fare;
    private LocalTime time;
    
	public Ticket() {
		super();
	}

	public Ticket(int ticketId, int stationIdFrom, int stationIdTo, float fare, LocalTime time) {
		super();
		this.ticketId = ticketId;
		this.stationIdFrom = stationIdFrom;
		this.stationIdTo = stationIdTo;
		this.fare = fare;
		this.time = time;
	}

	public int getTicketId() {
		return ticketId;
	}

	public void setTicketId(int ticketId) {
		this.ticketId = ticketId;
	}

	public int getStationIdFrom() {
		return stationIdFrom;
	}

	public void setStationIdFrom(int stationIdFrom) {
		this.stationIdFrom = stationIdFrom;
	}

	public int getStationIdTo() {
		return stationIdTo;
	}

	public void setStationIdTo(int stationIdTo) {
		this.stationIdTo = stationIdTo;
	}

	public float getFare() {
		return fare;
	}

	public void setFare(float fare) {
		this.fare = fare;
	}

	public LocalTime getTime() {
		return time;
	}

	public void setTime(LocalTime localTime) {
		this.time = localTime;
	}

	@Override
	public String toString() {
		return "Ticket [ticketId=" + ticketId + ", stationIdFrom=" + stationIdFrom + ", stationIdTo=" + stationIdTo
				+ ", fare=" + fare + ", time=" + time + "]";
	}

	
    
}
