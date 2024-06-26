/* eslint-disable react/prop-types */
import React, { useState, useEffect, createContext } from "react";
import { Navigate } from "react-router-dom";
import { Container } from "reactstrap";
import { NavMenu } from "../components/NavMenu";

const UserContext = createContext({});

function AuthorizeView(props) {
  const [authorized, setAuthorized] = useState(false);
  const [loading, setLoading] = useState(true); // add a loading state
  let emptyuser = { email: "" };

  const [user, setUser] = useState(emptyuser);

  useEffect(() => {
    // Get the cookie value
    let retryCount = 0; // initialize the retry count
    let maxRetries = 10; // set the maximum number of retries
    let delay = 1000; // set the delay in milliseconds

    // define a delay function that returns a promise
    function wait(delay) {
      return new Promise((resolve) => setTimeout(resolve, delay));
    }

    // define a fetch function that retries until status 200 or 401
    async function fetchWithRetry(url, options) {
      try {
        // make the fetch request
        let response = await fetch(url, options);

        // check the status code
        if (response.status == 200) {
          // console.log("Authorized");
          let j = await response.json();
          setUser({ email: j.email });
          setAuthorized(true);
          return response;
        } else if (response.status == 401) {
          // console.log("Unauthorized");
          return response;
        } else {
          // throw an error to trigger the catch block
          throw new Error("" + response.status);
        }
      } catch (error) {
        // increment the retry count
        retryCount++;
        // check if the retry limit is reached
        if (retryCount > maxRetries) {
          // stop retrying and rethrow the error
          throw error;
        } else {
          // wait for some time and retry
          await wait(delay);
          return fetchWithRetry(url, options);
        }
      }
    }

    // call the fetch function with retry logic
    fetchWithRetry("/account/pingauth", {
      method: "GET",
    })
      .catch((error) => {
        console.error(error.message);
      })
      .finally(() => {
        setLoading(false); // set loading to false when the fetch is done
      });
  }, []);

  if (loading) {
    return (
      <>
        <NavMenu />
        <Container>
          <p>Loading...</p>
        </Container>
      </>
    );
  } else {
    if (authorized && !loading) {
      return (
        <>
          <UserContext.Provider value={user}>
            <NavMenu pageTitle={props?.pageTitle} />
            <Container fluid={true}>{props?.children}</Container>
          </UserContext.Provider>
        </>
      );
    } else {
      return (
        <>
          <Navigate to="/login" />
        </>
      );
    }
  }
}

export function AuthorizedUser(props) {
  const user = React.useContext(UserContext);
  if (props?.value == "email") return <>{user.email}</>;
  else return <></>;
}

export function AuthorizedUserLogo(props) {
    const user = React.useContext(UserContext);
    if (props?.value == "email") {
        const logo = user.email?.split('@')[0].split('.').map(value => value[0]).join('').toUpperCase();
        return <>{logo}</>;
    }
    else return <></>;
  }

export default AuthorizeView;
