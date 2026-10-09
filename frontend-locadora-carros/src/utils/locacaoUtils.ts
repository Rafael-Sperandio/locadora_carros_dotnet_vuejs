
function dateToString(data: Date): string {
  const ano = data.getFullYear();
  const mes = String(data.getMonth() + 1).padStart(2, '0');
  const dia = String(data.getDate()).padStart(2, '0');

  return `${dia}/${mes}/${ano}`;
}

function converterParaData(data: Date | string): Date | null {
  if (data instanceof Date) {
    return new Date(
      data.getFullYear(),
      data.getMonth(),
      data.getDate()
    );
  }
  data = data?.replace('-', '/');
  const [dia, mes, ano] = data.split('/').map(Number);
  if (!ano || !mes || !dia) {
    return null;
  }

  return new Date(ano, mes, dia);
}

export function formatarData(data: string|Date|null): string {
  if (!data) return '';

  const [dia, mes, ano] = (typeof data === 'string' 
    ? data : dateToString(data)).split('/');

  return `${dia}/${mes}/${ano}`;
}
export function formatarDataAnoMesDia(data: string|Date): string {
  if (!data) return '';

  const [dia, mes, ano] = (typeof data === 'string' 
    ? data : dateToString(data)).split('/');

  return `${ano}/${mes}/${dia}`;
}

export function formatarMoeda(valor: number): string {
  return new Intl.NumberFormat('pt-BR', {
    style: 'currency',
    currency: 'BRL'
  }).format(valor);
}

export function quantidadeDias(
  dataInicio: Date|null,
  dataFim: Date|null
): number {
  if (!dataInicio || !dataFim) return 0;
  const inicio = converterParaData(dataInicio);
  const fim = converterParaData(dataFim);
  if (!inicio || !fim) return 0;
  const diferenca = fim.getTime() - inicio.getTime();
  return Math.floor(
    diferenca / (1000 * 60 * 60 * 24)
  ) + 1;
}


export function valorTotal(
  quantidadeDias: number,
  valorDiaria: number
): number {
  return quantidadeDias * valorDiaria;
}
