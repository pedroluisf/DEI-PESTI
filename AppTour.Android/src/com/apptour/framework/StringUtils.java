package com.apptour.framework;

import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;
import java.util.Map;

public class StringUtils {

	public static String GUID_EMPTY = "00000000-0000-0000-0000-000000000000";
	
	public static String join(List<String> pieces, String glue) {
		return join(pieces.iterator(), glue);
	}
	
	public static String join(Iterator<String> pieces, String glue) {
        StringBuilder s = new StringBuilder();
        while (pieces.hasNext()) {
            s.append(pieces.next());

            if (pieces.hasNext()) {
                s.append(glue);
            }
        }
        return s.toString();
    }
	
	public static String join(Map<String, String> pieces, String glue) {
        List<String> tmp = new ArrayList<String>(pieces.size());
        for (Map.Entry<String, String> entry : pieces.entrySet()) {
            tmp.add(entry.getKey() + ":" + entry.getValue());
        }
        return join(tmp, glue);
    }
}
