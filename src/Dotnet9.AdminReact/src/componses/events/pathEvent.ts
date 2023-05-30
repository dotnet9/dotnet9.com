import { EventEmitter } from 'events'

const PathEvent = new EventEmitter()

const BlogEvent = new EventEmitter()

export {
    PathEvent, 
    BlogEvent
}