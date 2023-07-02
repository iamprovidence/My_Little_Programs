import { Component } from 'react';
enum GoogleApiAuthScope {
    Events = 'https://www.googleapis.com/auth/calendar.events',
}

export default class App extends Component {

    const connectGoogleAccount = () => {

        const googleClientId = "123456789";

        const redirectUri = `${location.origin}/oauth/connect/google}`;// server side endpoint

        const scope = encodeURI(`${GoogleApiAuthScope.Events}`);

        const state = {
            data: "something useful",
        };
        const stateEncoded = encodeURI(JSON.stringify(state));

        const authLink = `https://accounts.google.com/o/oauth2/v2/auth?client_id=${googleClientId}&response_type=code&access_type=offline&redirect_uri=${redirectUri}&scope=${scope}&state=${stateEncoded}`;

        location.href = authLink;
    };

  render() {
      return (
          <button onClick={connectGoogleAccount}>
            Connect to Google
          <button />
    );
  }
}
