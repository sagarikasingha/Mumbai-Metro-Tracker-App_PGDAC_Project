package com.booking.repo;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import com.booking.entity.User;

@Repository
public interface UserRepository extends JpaRepository<User, Integer> {
   
}
