import styled from 'styled-components/native';
import { Pressable, Text } from 'react-native';

import { colors } from '../../style';

export const StyledButtonWrapper = styled.Pressable`
    width: 330px;
    height: 60px;
    margin-bottom: 10px;
    align-items: center;
    justify-content: center;
    border-radius: 8px;
    ${props => props.theme === 'light' ? 
    `
        background-color: ${colors.light.primary};
        border: 4px solid ${colors.light.border};
    `
    :
    `
        background-color: ${colors.dark.primary};
    `
}
`;

export const StyledButtonText = styled.Text`
    
`;