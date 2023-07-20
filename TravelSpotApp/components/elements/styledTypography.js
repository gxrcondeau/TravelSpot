import styled from 'styled-components/native';
import { Text } from 'react-native';

import { colors } from '../style';

//TODO Remove border after dev
export const StyledTitle = styled.Text`
    margin-bottom: 10px;
    font-size: 30px;
    font-family: ${props => props.italic ? 'Urbanist_600SemiBold_Italic' : 'Urbanist_600SemiBold'};
    ${props => props.theme === 'light' ? 
        `color: ${colors.light.text};`
        :
        `color: ${colors.dark.text};`
    }
    border: 1px dashed green;
`
export const StyledSubTitle = styled.Text`
    margin-bottom: 10px;
    font-size: 24px;
    font-family: 'Urbanist_600SemiBold';
    ${props => props.theme === 'light' ? 
        `color: ${colors.light.text};`
        :
        `color: ${colors.dark.text};`
    }
    border: 1px dashed green;
`

export const StyledText = styled.Text`
    margin-bottom: 10px;
    font-size: 16px;
    font-family: 'Urbanist_600SemiBold';
    ${props => props.theme === 'light' ? 
        `color: ${colors.light.text};`
        :
        `color: ${colors.dark.text};`
    }
    border: 1px dashed green;
`