FROM node:18-alpine

WORKDIR /app

COPY HealthyHabitHub/ .

ENV API_URL=https://localhost:5001

RUN npm install

RUN npm run build

EXPOSE 5173

CMD ["npm", "start"]