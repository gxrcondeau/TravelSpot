import styled from 'styled-components/native';
import { View } from 'react-native';

export const StyledTopbar = styled.View`
    height: 125px;
    max-width: 330px;
    width: 100%;
    padding: 70px 0 15px;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    position: absolute;
    top: 0;
    background: #ffffff;
    z-index: 1000;
`