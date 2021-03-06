/* Includes ------------------------------------------------------------------*/
#include <assert.h>
#include "ring_buffer.h"


bool RingBuffer_Init(RingBuffer *ringBuffer, char *dataBuffer, size_t dataBufferSize)
{
	assert(ringBuffer);
	assert(dataBuffer);
	assert(dataBufferSize > 0);

	if ((ringBuffer) && (dataBuffer) && (dataBufferSize > 0))
	{
	    ringBuffer->head = 0;
	    ringBuffer->tail = 0;
	    ringBuffer->empty = true;
	    ringBuffer->length = dataBufferSize;
	    ringBuffer->dataBuff = dataBuffer;
	    return true;
	}
	else
	{
	    return false;
	}
}

bool RingBuffer_Clear(RingBuffer *ringBuffer)
{
	assert(ringBuffer);

	if (ringBuffer)
	{
	    ringBuffer->tail = ringBuffer->head;
	    ringBuffer->empty = true;
	    return true;
	}
	else
	{
	    return false;
	}
}

bool RingBuffer_IsEmpty(const RingBuffer *ringBuffer)
{
  assert(ringBuffer);

  return ringBuffer->empty;
}

size_t RingBuffer_GetLen(const RingBuffer *ringBuffer)
{
	assert(ringBuffer);

	if (ringBuffer)
	{
	    if (ringBuffer->empty)
	    {
	        return 0;
	    }
	    else
	    {
	        return (ringBuffer->head - ringBuffer->tail + 1);
	    }
	}
	else
	{
	    return 0;
	}
}

size_t RingBuffer_GetCapacity(const RingBuffer *ringBuffer)
{
	assert(ringBuffer);

	if (ringBuffer) {
		return ringBuffer->length;
	}
	else
	{
	    return 0;
	}
}


bool RingBuffer_PutChar(RingBuffer *ringBuffer, char c)
{
	assert(ringBuffer);

	if (ringBuffer)
	{
			if (ringBuffer->empty)
			{
					ringBuffer->empty = false;
					ringBuffer->dataBuff[ringBuffer->head] = c;
					return true;
			}
			else
			{
					if (((ringBuffer->head + 1) % ringBuffer->length) == ringBuffer->tail)
					{
							return false;
					}
					else
					{
							ringBuffer->head = (ringBuffer->head + 1) % ringBuffer->length;
							ringBuffer->dataBuff[ringBuffer->head] = c;
							return true;
					}
			}
	}
	else
	{
	    return false;
	}
}

bool RingBuffer_GetChar(RingBuffer *ringBuffer, char *c)
{
	assert(ringBuffer);
	assert(c);

	if ((ringBuffer) && (c) && !ringBuffer->empty)
	{
			if (ringBuffer->tail == ringBuffer->head)
			{
					*c = ringBuffer->dataBuff[ringBuffer->tail];
					ringBuffer->empty = true;
					return true;
			}
			else
			{
				*c = ringBuffer->dataBuff[ringBuffer->tail];
				ringBuffer->tail = (ringBuffer->tail + 1) % ringBuffer->length;
				return true;
			}
	}
	else
	{
	    return false;
	}
}
