import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.jsx'
import './global/global.css'
import 'bootstrap/dist/css/bootstrap.css'
import { ViewportProvider } from './contexts/viewportContext.jsx'
import { AuthContextProvider } from './contexts/authContext.jsx'

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <AuthContextProvider>
      <ViewportProvider>
        <App />
      </ViewportProvider>
    </AuthContextProvider>
  </React.StrictMode>,
)
