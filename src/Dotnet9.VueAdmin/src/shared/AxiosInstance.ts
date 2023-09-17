import axios, { RawAxiosRequestHeaders } from 'axios'

const headers: RawAxiosRequestHeaders = {}

const instance = axios.create({
    baseURL: 'https://some-domain.com/api/',
    timeout: 1000,
    headers: headers
});

headers[""] = ""

// serviceOptions.axios = instance