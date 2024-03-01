
// import React from "react";
// import "bootstrap/dist/css/bootstrap.min.css"; // Import Bootstrap styles
// import "./Header.css"; // Import CSS file
// import myImage from "../styles/site_logo.png";

// const Header = () => {
//   return (
//     <div>
//       <header>
//         {/* <img src="" alt="not found" srcSet="" /> */}
//         <img src={myImage} alt="Description of the image" />
//         <nav>
//           <ul className="navigate_links">
//           <li><a href="../../../MumbaiMetro/MyWebApp/wwwroot/index.html">Home</a></li>
//           <li><a href="./about.html">About</a></li>
//           <li><a href="./blog.html">Blog</a></li>
//           <li><a href="http://localhost:5109/Home/FeedbackForm">Feedback</a></li>
//           </ul>
//         </nav>
//         <a className="signin" href="http://localhost:5109/Home/UserLogin">
//           <button>User Login</button>
//         </a>
//         <a
//           className="signin"
//           href="http://localhost:5109/Home/UserRegistration"
//         >
//           <button>User Registration</button>
//         </a>
//         <a className="signin" href="http://localhost:5109/Home/AdminLogin">
//           <button>Admin Login</button>
//         </a>
//       </header>
//     </div>
//   );
// };

// export default Header;

import React from "react";
import { BrowserRouter, Routes, Route, Link } from 'react-router-dom';
import "bootstrap/dist/css/bootstrap.min.css"; // Import Bootstrap styles
import "./Header.css"; // Import CSS file
import myImage from "../styles/site_logo.png";

const path = "../../../MumbaiMetro/MyWebApp/wwwroot";
const Header = () => {
  return (
    <div>
      <header>
      <img src={myImage} alt="Description of the image" />
        <nav>
          <ul className="navigate_links">
          <li><a href="http://localhost:5109/#">Home</a></li>
         <li><a href="http://localhost:5109/about.html">About</a></li>
           <li><a href="http://localhost:5109/blog.html">Blog</a></li>
           <li><a href="http://localhost:5109/Home/FeedbackForm">Feedback</a></li>
          </ul>
        </nav>
        <a className="signin" href="http://localhost:5109/Home/UserLogin">
       <button>User Login</button>
     </a>
       <a
          className="signin"
          href="http://localhost:5109/Home/UserRegistration"
        >
          <button>User Registration</button>
        </a>
        <a className="signin" href="http://localhost:5109/Home/AdminLogin">
          <button>Admin Login</button>
        </a>
      </header>
    </div>
  );
};

export default Header;
