FROM node:18-alpine

WORKDIR /app

COPY HealthyHabitHub/ .

RUN npm install

RUN npm run build

EXPOSE 5173

CMD ["npm", "start"]