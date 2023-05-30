import React, { Component } from 'react'
import './index.css';

export default class Ide extends Component {

    render() {
        return (
            <iframe style={{height:"100%",width:'100%'}} src='https://webassembly.tokengo.top:8843/?home=https://blog.tokengo.top'></iframe>
        )
    }
}
