import './App.css';
import { useState, useEffect } from 'react';

import { Swiper, SwiperSlide } from 'swiper/react';

function App() {
  const [data, setData] = useState([]);  

  const updateData = () => {
    fetch('http://localhost:6981/api/products')
      .then(res => res.json())
      .then(json => setData(json))
      .catch(error => console.error(error));
  };

  useEffect(() => {
    updateData();
    let interval = setInterval(() => updateData(), 60000);
    return () => clearInterval(interval);
  }, []);
  
  return (
    <Swiper
      slidesPerView={1}
      navigation
      autoplay={{delay: 5000}}
      speed={10}
      loop={true}
    >
      {data.map((item) => (
        <SwiperSlide key={item.name}>
          <div className='slide-container'>
            <img
              className='product-image' 
              src={item.imageUrl} 
              alt='Swipe'
            />
            <div className='product-name'>
              <span>{item.name}</span>
              {
              item.price &&
              <span>: R$ {item.price}</span>
            }
            </div>

          </div>
        </SwiperSlide>          
      ))}

    </Swiper>
  );
}



export default App;
