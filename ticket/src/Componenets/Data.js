
import React, { useState } from "react";
import axios from "axios";
import "bootstrap/dist/css/bootstrap.min.css"; // Import Bootstrap styles
import myImage from "../Designer (19).jpeg";
const Data = () => {
  const [routeId, setRouteId] = useState("");
  const [stationIdFrom, setStationIdFrom] = useState("");
  const [stationIdTo, setStationIdTo] = useState("");
  const [email, setEmail] = useState("");
  const [ticket, setTicket] = useState(null);
  const [paymentDone, setPaymentDone] = useState(false);

  const handlePayNow = () => {
    // Validation logic here
    sendTestEmail();
  };

  const sendTestEmail = async () => {
    try {
      // Send request to send the test email in the background
      const emailResponse = await axios.get(
        `http://localhost:5000/send-test-email?to=${email}`
      );

      // Prompt for OTP entry immediately
      var otp = prompt("Enter OTP:");
      if (otp !== null) {
        console.log("OTP entered:", otp); // Log the OTP entered
        await verifyOTP(otp); // Call verifyOTP after OTP is entered
      }

      // Wait for the email request to complete (optional, depending on the use case)
      await emailResponse;
    } catch (error) {
      console.error("Error sending email:", error);
    }
  };

  const verifyOTP = (otp) => {
    // Pass otp as an argument
    const newotp = {
      enteredOTP: otp,
    };

    axios
      .post("http://localhost:5000/verify-otp", newotp)
      .then((response) => {
        console.log(response.data);
        if (response.data === "Entered OTP is valid.") {
          generateTicket();
        } else {
          alert("Invalid OTP. Please try again.");
        }
      })
      .catch((error) => {
        console.error("Error verifying OTP:", error);
      });
  };

  const generateTicket = () => {
    // Call the endpoint to generate ticket
    axios
      .post("http://localhost:5000/tickets/book", {
        routeId: parseInt(routeId),
        stationIdFrom: parseInt(stationIdFrom),
        stationIdTo: parseInt(stationIdTo),
      })
      .then((response) => {
        console.log("Ticket generated:", response.data);
        setTicket(response.data);
        setPaymentDone(true); // Set payment done to true after ticket is generated
      })
      .catch((error) => {
        console.error("Error generating ticket:", error);
      });
  };

  return (
    <div>
      <section className="contact-page-section">
        <div className="container">
          <div className="sec-title">
            <div className="title">
              Experience unforgettable moments. Book your tickets now.
            </div>
            <h2>FASTEST METRO TICKET BOOKING</h2>
          </div>
          <div className="inner-container row">
            <div className="form-column col-md-8 col-sm-12 col-xs-12">
              <div className="inner-column">
                <div className="contact-form">
                  <form>
                    <div className="row clearfix">
                      <div className="form-group col-md-12 col-sm-12 co-xs-12">
                        <label htmlFor="route_id">Route ID:</label>
                        <br />
                        <select
                          id="route_id"
                          name="route_id"
                          value={routeId}
                          onChange={(e) => setRouteId(e.target.value)}
                          required
                        >
                          <option value="">Select Route</option>
                          <option value="1">Yellow</option>
                          <option value="2">Red</option>
                          <option value="3">Blue</option>
                        </select>
                      </div>

                      <div className="form-group col-md-6 col-sm-6 co-xs-12 dropdown">
                        <label htmlFor="source">Source:</label>
                        <br />
                        <select
                          id="source"
                          name="source"
                          value={stationIdFrom}
                          onChange={(e) => setStationIdFrom(e.target.value)}
                          required
                        >
                          <option value="">Select Source</option>
                          <option value="1">Andheri West</option>
                          <option value="2">Lower Oshiwara</option>
                          <option value="3">Oshiwara</option>
                          <option value="4">Goregaon West</option>
                        <option value="5">Pahadi Goregaon</option>
                        <option value="6">Lower Malad</option>
                        <option value="7">Malad West</option>
                        <option value="8">Valnai</option>
                        <option value="9">Dahanukarwadi</option>
                        <option value="10">Ekta Nagar</option>
                        <option value="11">Kastur Park</option>
                        <option value="12">Don Bosco</option>
                        <option value="13">Ektar LIC Colony</option>
                        <option value="14">I.C.Colony</option>
                        <option value="15">Rushi Sankul</option>
                        <option value="16">Anand Nager</option>
                        <option value="17">Dashisar</option>
                        <option value="19">Ovaripada</option>
                        <option value="20">National Park</option>
                        <option value="21">Devipada</option>
                        <option value="22">Magathane</option>
                        <option value="23">Mahindra</option>
                        <option value="24">Bandongri/Ankurli</option>
                        <option value="25">Kurar Village</option>
                        <option value="26">Dindoshi</option>
                        <option value="27">Aarey Road Junction</option>
                        <option value="28">Goregaon East</option>
                        <option value="29">Jogeshwari East</option>
                        <option value="30">Mogra</option>
                        <option value="31">Gundavli</option>
                        <option value="32">Versova</option>
                        <option value="33">D.N. Nagar</option>
                        <option value="34">Azad Nagar</option>
                        <option value="35">Andheri</option>
                        <option value="36">Western Express Highway</option>
                        <option value="37">Chakala</option>
                        <option value="38">Airport Road</option>
                        <option value="39">Marol Naka</option>
                        <option value="40">Saki Naka</option>
                        <option value="41">Asalpha</option>
                        <option value="42">Jagruti Nagar</option>
                        <option value="43">Ghatkopar</option>
                      
                        </select>
                      </div>

                      <div className="form-group col-md-6 col-sm-6 co-xs-12">
                        <label htmlFor="destination">Destination:</label>
                        <br />
                        <select
                          id="destination"
                          name="destination"
                          value={stationIdTo}
                          onChange={(e) => setStationIdTo(e.target.value)}
                          required
                        >
                          <option value="">Select Destination</option>
                          <option value="1">Andheri West</option>
                          <option value="2">Lower Oshiwara</option>
                          <option value="3">Oshiwara</option>
                          <option value="4">Goregaon West</option>
//                         <option value="5">Pahadi Goregaon</option>
//                         <option value="6">Lower Malad</option>
//                         <option value="7">Malad West</option>
//                         <option value="8">Valnai</option>
//                         <option value="9">Dahanukarwadi</option>
//                         <option value="10">Ekta Nagar</option>
//                         <option value="11">Kastur Park</option>
//                         <option value="12">Don Bosco</option>
//                         <option value="13">Ektar LIC Colony</option>
//                         <option value="14">I.C.Colony</option>
//                         <option value="15">Rushi Sankul</option>
//                         <option value="16">Anand Nager</option>
//                         <option value="17">Dashisar</option>
//                         <option value="19">Ovaripada</option>
//                         <option value="20">National Park</option>
//                         <option value="21">Devipada</option>
//                         <option value="22">Magathane</option>
//                         <option value="23">Mahindra</option>
//                         <option value="24">Bandongri/Ankurli</option>
//                         <option value="25">Kurar Village</option>
//                         <option value="26">Dindoshi</option>
//                         <option value="27">Aarey Road Junction</option>
//                         <option value="28">Goregaon East</option>
//                         <option value="29">Jogeshwari East</option>
//                         <option value="30">Mogra</option>
//                         <option value="31">Gundavli</option>
//                         <option value="32">Versova</option>
//                         <option value="33">D.N. Nagar</option>
//                         <option value="34">Azad Nagar</option>
//                         <option value="35">Andheri</option>
//                         <option value="36">Western Express Highway</option>
//                         <option value="37">Chakala</option>
//                         <option value="38">Airport Road</option>
//                         <option value="39">Marol Naka</option>
//                         <option value="40">Saki Naka</option>
//                         <option value="41">Asalpha</option>
//                         <option value="42">Jagruti Nagar</option>
//                         <option value="43">Ghatkopar</option>
//                       
                        </select>
                      </div>

                      <div className="form-group col-md-12 col-sm-12 co-xs-12">
                        <label htmlFor="email">Email:</label>
                        <br />
                        <input
                          type="email"
                          value={email}
                          onChange={(e) => setEmail(e.target.value)}
                          required
                        />
                      </div>

                      <div className="form-group col-md-12 col-sm-12 co-xs-12">
                        <button
                          type="button"
                          onClick={handlePayNow}
                          className="theme-btn btn-style-one"
                        >
                          Buy
                        </button>
                        {paymentDone && (
                          <div>
                            <h2>Payment Done!</h2>
                          </div>
                        )}
                        {ticket && (
                          <div>
                            <h2>Your Ticket</h2>
                            <p>
                              <strong>Ticket ID:</strong> {ticket.ticketId}
                              <br />
                              <strong>From Station ID:</strong>{" "}
                              {ticket.stationIdFrom}
                              <br />
                              <strong>To Station ID:</strong>{" "}
                              {ticket.stationIdTo}
                              <br />
                              <strong>Fare:</strong> {ticket.fare}
                              <br />
                              <strong>Time:</strong> {ticket.time}
                              <br />
                            </p>
                          </div>
                        )}
                      </div>
                    </div>
                  </form>
                </div>
              </div>
            </div>
            <div className="col-md-4 col-sm-12 col-xs-12">
              <img
                src={myImage}
                alt="ticket"
                height="530"
                width="450"
              />
            </div>
          </div>
        </div>
      </section>
    </div>
  );
};

export default Data;
