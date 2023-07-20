import styled from 'styled-components/native';
import { View } from 'react-native';

import { colors } from '../style';

export const SafeAreaContainer = styled.View`
    padding: 30px 30px 0;
    align-items: center;
    ${props => props.theme === 'light' ? 
        `background-color: ${colors.light.background};`
        :
        `background-color: ${colors.dark.background};`
    }
`

//TODO Remove border after dev
export const StyledContainer = styled.View`
    width: 100%;
    height: 100%;
    ${props => props.theme === 'light' ? 
        `
            background-color: ${colors.light.background};
        `
        :
        `
            background-color: ${colors.dark.background};
        `
    }
    border: 1px dashed red;
`; 

export const StyledMessagebox = styled.View`
    max-width: 330px;
    width: 100%;
    margin: 10px auto;
    padding: 10px 5px;
    ${props => props.theme === 'light' ? 
        `
            background-color: ${colors.light.primary};
            border: 2px solid ${colors.light.border};
        `
        :
        `
            background-color: ${colors.dark.primary};
            border: 2px solid ${colors.dark.border};
        `
    }
`;