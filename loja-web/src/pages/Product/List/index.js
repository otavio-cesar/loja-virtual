import { useEffect, useState } from "react";
import { useHistory } from "react-router";
import api from "../../../services/api";
import './styles.css';

export default function ProductList() {
    const [produtos, setProdutos] = useState([]);
    const history = useHistory();

    useEffect(() => {
        carregaProdutos()
    }, [])

    async function carregaProdutos() {
        await api.get('produto')
            .then((res) => {
                setProdutos(res.data)
            }).catch(e => {
                alert(e)
            });
    }

    async function handleClick(produto) {
        history.push(`produto/${produto.CodProduto}`, { produto })
    }

    return (
        <div className="content">
            <div className="actions">
                <button className="button" onClick={() => history.push('produto')}>Novo produto</button>
            </div>
            <div className="linha header">
                <span className="coluna header">Código</span>
                <span className="coluna header">Descrição</span>
                <span className="coluna header">Quantidade</span>
                <span className="coluna header">Preço</span>
            </div>
            {produtos && produtos.map(p => {
                return (
                    <div className="linha" onClick={() => handleClick(p)}>
                        <span className="coluna">{p.CodProduto}</span>
                        <span className="coluna">{p.Descricao}</span>
                        <span className="coluna">{p.Qtde}</span>
                        <span className="coluna">
                            {Intl.NumberFormat('pt-BR', {
                                style: 'currency',
                                currency: 'BRL'
                            }).format(p.Preco)}
                        </span>
                    </div>
                )
            })}
        </div>
    );
}