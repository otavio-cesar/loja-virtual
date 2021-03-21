import React, { useEffect, useRef, useState } from 'react';
import { useHistory, useParams } from 'react-router-dom';
import './styles.css';
import api from '../../../services/api'

export default function ProductSave(props) {
    const [codigo, setCodigo] = useState('');
    const [quantidade, setQuantidade] = useState('');
    const [descricao, setDescricao] = useState('');
    const [preco, setPreco] = useState('');
    const history = useHistory();

    let { id } = useParams();

    useEffect(() => {
        if (id) {
            var produto = props.location.state.produto
            setCodigo(id)
            setQuantidade(produto.Qtde)
            setPreco(produto.Preco)
            setDescricao(produto.Descricao)
        }
    }, [id])

    async function handleRegister(e) {
        e.preventDefault();

        const data = {
            CodProduto: +codigo,
            Descricao: descricao,
            Qtde: +quantidade,
            Preco: +preco,
            PromocaoAtiva: props.location.state?.produto.PromocaoAtiva
        }

        if (!id) {
            await api.post('produto', data)
                .then(() => {
                    alert('Produto cadastrado!')
                    history.push('/')
                }).catch((data) => {
                    var statusCode = data.response?.status
                    if (statusCode == 400) {
                        var errorMessage = data.response.data.message
                        alert(errorMessage)
                    } else if (statusCode == 500) {
                        alert('Ocorreu um erro no servidor')
                    } else {
                        alert(data)
                    }
                });
        } else {
            await api.put(`produto/${id}`, data)
                .then(() => {
                    alert('Produto atualizado!')
                    history.push('/')
                }).catch((data) => {
                    var statusCode = data.response?.status
                    if (statusCode == 400) {
                        var errorMessage = data.response.data.message
                        alert(errorMessage)
                    } else if (statusCode == 500) {
                        alert('Ocorreu um erro no servidor')
                    } else {
                        alert(data)
                    }
                });
        }
    }

    async function handleExcluir(e) {
        e.preventDefault();

        var confirmar = prompt("Digite 'Sim' para excluir", "Sim")
        if (confirmar != 'Sim')
            return

        await api.delete(`produto/${id}`)
            .then(() => {
                alert('Produto excluído!')
                history.push('/')
            }).catch((data) => {
                var statusCode = data.response?.status
                if (statusCode == 400) {
                    var errorMessage = data.response.data.message
                    alert(errorMessage)
                } else if (statusCode == 500) {
                    alert('Ocorreu um erro no servidor')
                } else {
                    alert(data)
                }
            });
    }

    return (
        <div className="register-container">
            <div className="content">
                <form onSubmit={handleRegister}>
                    {id && <>
                        <span>Código:</span>
                        <input
                            required
                            type="number"
                            value={id}
                            disabled
                            onChange={e => setCodigo(e.target.value)} />
                    </>}

                    <span>Descrição:</span>
                    <textarea
                        required
                        value={descricao}
                        onChange={e => setDescricao(e.target.value)} />

                    <div className="input-group">
                        <span>Quantidade:</span>
                        <span>Preço: (R$)</span>
                    </div>

                    <div className="input-group">
                        <input
                            required
                            type="number"
                            min="0"
                            step="1"
                            value={quantidade}
                            onChange={e => setQuantidade(e.target.value)} />

                        <input
                            min="0"
                            required
                            step="0.01"
                            type="number"
                            value={preco}
                            onChange={e => setPreco(e.target.value)} />
                    </div>

                    {!id ? <button className="button" type="submit">Cadastrar</button>
                        :
                        <>
                            <button className="button" type="submit">Editar</button>
                            <button className="button" onClick={(e) => handleExcluir(e)}>Excluir</button>
                        </>
                    }

                </form>
            </div>
        </div>
    );
}