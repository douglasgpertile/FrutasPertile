FROM node:18-alpine AS build
WORKDIR /src
COPY . .
RUN npm i
RUN npm run build


FROM node:18-alpine
RUN npm install -g serve
WORKDIR /app
COPY --from=build /src/build .
CMD serve -s -p 6980 
EXPOSE 6980