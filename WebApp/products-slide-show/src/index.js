import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';

import './index.css';

import 'swiper/css';
import 'swiper/css/navigation';
import 'swiper/css/pagination';
import 'swiper/css/scrollbar';

import { register } from 'swiper/element/bundle';

register();




const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>
);
