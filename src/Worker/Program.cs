using KeepAlive;

int contador = 0;
while (true)
{
    PowerUtilities.PreventPowerSave(contador);
    contador++;
}