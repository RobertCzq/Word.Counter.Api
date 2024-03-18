Word.Counter.Api

I decided to implement this as an api endpoint call.
You can test the code by running it, going to the GetWordCountFile endpoint and uploading your text file.
Obs: It only works with .txt files.

I decided to go for an approach where I use a dictionary with a few tweaks.
I am using the word as the key, ignoring the case, and the value is the count of words found in the file.
I did try other approaches where I used parallelization with a concurrent dictionary, and another one using a prefix tree, but in the end, those approaches turned out to be less performant than my current implementation.

I have added what I would consider minimal testing for this project.

The returned file it's just my dictionary serialized, I did not bother making the formatting more beautiful than that.
The returned file contains the words ordered by descending count.
 