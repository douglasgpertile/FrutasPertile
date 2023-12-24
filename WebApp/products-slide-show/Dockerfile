FROM node:18-alpine AS base
RUN npm install -g serve
WORKDIR /app
EXPOSE 6980

FROM node:18-alpine AS build
WORKDIR /src
COPY package*.json ./
RUN npm ci

FROM build AS publish
COPY . .
RUN npm run build

FROM base AS final
COPY --from=publish /src/build .
CMD serve -s -p 6980